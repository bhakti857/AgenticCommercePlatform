using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using AI_Ecommerce.Agent.Harness;

namespace AI_Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgentController : ControllerBase
{
    private readonly AgentHarness _agent;

    public AgentController(AgentHarness agent)
    {
        _agent = agent;
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
        public string? SessionId { get; set; }
    }

    [HttpPost("chat")]
    [EnableRateLimiting("agent-chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        // Only employees may talk to the agent — customers are rejected outright.
        var accountType = User.FindFirst("AccountType")?.Value;
        if (accountType != "Employee")
        {
            return Forbid();
        }

        // Get the current user ID from the JWT token
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        // Generate a session ID if not provided
        var sessionId = request.SessionId ?? Guid.NewGuid().ToString();

        // Only MasterAdmin (1) / Admin (2) users may let the agent write files or
        // execute shell commands — everyone else gets read-only tools. This limits
        // the blast radius of the API's auto-approve ApprovalHandler (see Program.cs).
        var userTypeClaim = User.FindFirst("UserTypeId")?.Value;
        var allowWriteTools = userTypeClaim == "1" || userTypeClaim == "2";

        // Process the message through the agent harness
        var response = await _agent.ProcessMessageAsync(userId, request.Message, sessionId, allowWriteTools);

        return Ok(new
        {
            Response = response,
            SessionId = sessionId
        });
    }
}