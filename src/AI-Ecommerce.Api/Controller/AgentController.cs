using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using AI_Ecommerce.Agent.Harness;
using AI_Ecommerce.Api.Services;

namespace AI_Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgentController : ControllerBase
{
    private readonly AgentHarness _agent;
    private readonly ApprovalGate _approvals;

    public AgentController(AgentHarness agent, ApprovalGate approvals)
    {
        _agent = agent;
        _approvals = approvals;
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
        public string? SessionId { get; set; }
    }

    public class ApproveRequest
    {
        public bool Approved { get; set; }
    }

    [HttpPost("chat")]
    [EnableRateLimiting("agent-chat")]
    [Authorize(Policy = "RequireEmployee")]
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

        // Only MasterAdmin (1) / Admin (2) users may let the agent write files or
        // execute shell commands — everyone else gets read-only tools. Any such
        // operation is parked in the ApprovalGate until an admin approves it
        // (see POST api/agent/approvals/{token}).
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

    /// <summary>Lists WriteFile/ExecuteCommand operations awaiting approval (employees only).</summary>
    [HttpGet("approvals")]
    [Authorize(Policy = "RequireEmployee")]
    public IActionResult ListApprovals()
    {
        return Ok(new { Pending = _approvals.ListPending() });
    }

    /// <summary>
    /// Resolves a pending operation by its token. Approving unblocks the waiting
    /// conversation so the write/command executes; denying cancels it. Only
    /// MasterAdmin/Admin may approve (matches who is allowed to trigger writes).
    /// </summary>
    [HttpPost("approvals/{token}")]
    [Authorize(Policy = "MasterAdminOrAdmin")]
    public IActionResult ResolveApproval(string token, [FromBody] ApproveRequest request)
    {
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest("Approval token is required.");

        if (!_approvals.Decide(token, request.Approved))
            return NotFound(new { error = "Unknown or already-resolved approval token." });

        return Ok(new
        {
            Token = token,
            Approved = request.Approved,
            Message = request.Approved
                ? "Approved — the pending operation will proceed."
                : "Denied — the pending operation was cancelled."
        });
    }
}