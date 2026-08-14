using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace AI_Ecommerce.Agent.Tools
{
    public static class DevTools
    {
        // Set by the host (CLI, web API, etc.) to ask the user for approval.
        // Return true to proceed, false to cancel.
        public static Func<string, Task<bool>>? ApprovalHandler { get; set; }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private static readonly string _projectRoot = FindProjectRoot();

        private static string FindProjectRoot()
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (dir != null && !dir.GetFiles("*.slnx").Any() && !dir.GetFiles("*.sln").Any())
            {
                dir = dir.Parent;
            }

            return dir?.FullName
                ?? throw new DirectoryNotFoundException("Could not locate solution root (no .slnx/.sln file found in any parent directory).");
        }

        [Description("Read the content of a file in the project.")]
        public static async Task<string> ReadFile(
            [Description("Relative path to the file (e.g., 'src/AI-Ecommerce.Api/Program.cs')")]
            string filePath)
        {
            var fullPath = Path.Combine(_projectRoot, filePath);
            if (!File.Exists(fullPath))
                return $"File not found: {filePath}";

            var content = await File.ReadAllTextAsync(fullPath);
            return content;
        }

        [Description("Write or overwrite a file in the project.")]
        public static async Task<string> WriteFile(
            [Description("Relative path to the file")] string filePath,
            [Description("Content to write to the file")] string content)
        {
            if (ApprovalHandler != null)
            {
                var approved = await ApprovalHandler($"Write to file '{filePath}' ({content.Length} characters)?");
                if (!approved)
                    return $"Write to '{filePath}' was cancelled by the user.";
            }

            var fullPath = Path.Combine(_projectRoot, filePath);
            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(fullPath, content);
            return $"Successfully wrote file: {filePath} ({content.Length} characters)";
        }

        [Description("List files and directories in a given project path.")]
        public static async Task<string> ListDirectory(
            [Description("Relative path to directory, empty for root")] string path = "")
        {
            var fullPath = Path.Combine(_projectRoot, path);
            if (!Directory.Exists(fullPath))
                return $"Directory not found: {path}";

            var entries = Directory.GetFileSystemEntries(fullPath)
                .Select(e => new
                {
                    Name = Path.GetFileName(e),
                    IsDirectory = Directory.Exists(e),
                    FullPath = Path.GetRelativePath(_projectRoot, e)
                })
                .OrderBy(e => e.IsDirectory ? 0 : 1)
                .ThenBy(e => e.Name);

            return JsonSerializer.Serialize(entries, _jsonOptions);
        }

        [Description("Search for text within project files (grep-like).")]
        public static async Task<string> SearchCode(
            [Description("Search term (case-insensitive)")] string searchTerm,
            [Description("File extension filter, e.g., '.cs' or '.json' (optional)")] string? extension = null)
        {
            var results = new List<string>();
            var files = Directory.GetFiles(_projectRoot,
                extension != null ? $"*{extension}" : "*",
                SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (file.Contains("bin") || file.Contains("obj") || file.Contains("node_modules"))
                    continue;

                var content = await File.ReadAllTextAsync(file);
                if (content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    var relativePath = Path.GetRelativePath(_projectRoot, file);
                    results.Add($"{relativePath}: found '{searchTerm}'");
                }
            }

            return results.Count == 0
                ? "No matches found."
                : string.Join("\n", results.Take(50));
        }

        [Description("Execute a command line command (e.g., 'dotnet build', 'git status').")]
        public static async Task<string> ExecuteCommand(
            [Description("Command to execute, e.g., 'dotnet build'")] string command)
        {
            if (ApprovalHandler != null)
            {
                var approved = await ApprovalHandler($"Run command: '{command}'?");
                if (!approved)
                    return $"Command '{command}' was cancelled by the user.";
            }

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = _projectRoot,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            return string.IsNullOrEmpty(error)
                ? output
                : $"Output:\n{output}\n\nError:\n{error}";
        }
    }
}