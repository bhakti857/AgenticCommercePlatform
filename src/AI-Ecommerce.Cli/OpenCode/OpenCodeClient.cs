using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AI_Ecommerce.Cli.OpenCode;

/// <summary>
/// Minimal HTTP client for the opencode server REST API
/// (see https://opencode.ai/docs/server).
/// </summary>
public sealed class OpenCodeClient
{
    private readonly HttpClient _http;

    public OpenCodeClient(string baseUrl, string? password = null)
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/")
        };

        if (!string.IsNullOrEmpty(password))
        {
            var token = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"opencode:{password}"));
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", token);
        }
    }

    /// <summary>Checks that the opencode server is reachable.</summary>
    public async Task<bool> IsHealthyAsync(CancellationToken ct = default)
    {
        try
        {
            var node = JsonNode.Parse(await _http.GetStringAsync("global/health", ct).ConfigureAwait(false));
            return node?["healthy"]?.GetValue<bool>() == true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>Creates a fresh opencode session and returns its id.</summary>
    public async Task<string> CreateSessionAsync(CancellationToken ct = default)
    {
        var response = await _http.PostAsJsonAsync("session", new { }, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var node = JsonNode.Parse(await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false));
        return node!["id"]!.GetValue<string>();
    }

    /// <summary>Lists existing session ids (newest first).</summary>
    public async Task<List<string>> ListSessionIdsAsync(CancellationToken ct = default)
    {
        var sessions = await _http.GetFromJsonAsync<List<JsonNode>>("session", ct).ConfigureAwait(false) ?? new();
        return sessions.Select(s => s["id"]!.GetValue<string>()).ToList();
    }

    /// <summary>Sends a user message to a session and returns the assistant's text reply.</summary>
    public async Task<string> SendMessageAsync(string sessionId, string text, CancellationToken ct = default)
    {
        var body = new { parts = new[] { new { type = "text", text } } };
        var response = await _http.PostAsJsonAsync($"session/{sessionId}/message", body, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var node = JsonNode.Parse(await response.Content.ReadAsStringAsync(ct).ConfigureAwait(false));

        var texts = node?["parts"]?
            .AsArray()
            .Where(p => p?["type"]?.GetValue<string>() == "text" && p["text"] != null)
            .Select(p => p!["text"]!.GetValue<string>());

        var reply = texts is null ? null : string.Join("\n", texts);
        return string.IsNullOrWhiteSpace(reply) ? "No response generated" : reply;
    }
}
