-- ============================================================
-- Page: AI Agent Chat
-- Route: /agent
-- Access: Employee account only (JWT AccountType = "Employee").
--         Write/exec tools additionally require UserTypeId 1 or 2.
-- API: POST /api/agent/chat
-- Status: BUILT
-- NOTE: The agent's tool calls (ReadFile/ListDirectory/SearchCode/
--       WriteFile/ExecuteCommand) operate on the filesystem, not the DB.
--       Only the conversation transcript touches the database.
-- Tables used: ConversationHistory (SELECT + INSERT)
-- ============================================================

-- 1. Load prior messages for the session (last 20 only, oldest first).
--    If no rows exist yet, the system prompt is inserted first
--    (AgentHarness.LoadHistoryAsync).
SELECT SessionId, UserId, Role, Content, CreatedAt
FROM   ConversationHistory
WHERE  SessionId = @SessionId
ORDER  BY CreatedAt;

-- 2. Persist every chat turn (system seed, user message, assistant reply).
--    Role is one of 'system' | 'user' | 'assistant'.
INSERT INTO ConversationHistory (Id, SessionId, UserId, Role, Content, CreatedAt)
VALUES (NEWID(), @SessionId, @UserId, @Role, @Content, SYSUTCDATETIME());