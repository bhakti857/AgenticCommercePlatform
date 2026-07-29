using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        // Get the current user ID from the JWT token
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        // Generate a session ID if not provided
        var sessionId = request.SessionId ?? Guid.NewGuid().ToString();

        // Process the message through the agent harness
        var response = await _agent.ProcessMessageAsync(userId, request.Message, sessionId);

        return Ok(new
        {
            Response = response,
            SessionId = sessionId
        });
    }
}