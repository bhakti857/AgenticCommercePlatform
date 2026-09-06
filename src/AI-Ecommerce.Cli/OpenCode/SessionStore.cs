using System.Text.Json;

namespace AI_Ecommerce.Cli.OpenCode;

/// <summary>
/// Persists the active opencode session id to disk so that conversation
/// history is resumed across CLI restarts.
/// </summary>
internal static class SessionStore
{
    private static readonly string FilePath = Path.Combine(
        Path.GetDirectoryName(typeof(SessionStore).Assembly.Location) ?? ".",
        ".opencode-session");

    /// <summary>Reads the saved session id, or null if none exists.</summary>
    public static string? Load()
    {
        try
        {
            if (!File.Exists(FilePath)) return null;
            var json = File.ReadAllText(FilePath);
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.TryGetProperty("sessionId", out var id)
                ? id.GetString()
                : null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>Saves the session id for the next run.</summary>
    public static void Save(string sessionId)
    {
        try
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(new { sessionId }));
        }
        catch
        {
            // Persistence is best-effort; failing is not fatal.
        }
    }
}
