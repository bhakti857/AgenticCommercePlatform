import { useEffect, useRef, useState } from 'react';
import api from '../../api/client';

interface ChatMessage {
  role: 'user' | 'assistant';
  content: string;
}

interface PendingApproval {
  token: string;
  description: string;
  createdAt: string;
}

const SESSION_KEY = 'agentSessionId';

export default function Chat() {
  const [message, setMessage] = useState('');
  const [history, setHistory] = useState<ChatMessage[]>([]);
  const [sessionId, setSessionId] = useState(() => localStorage.getItem(SESSION_KEY) || '');
  const [loading, setLoading] = useState(false);
  const [pending, setPending] = useState<PendingApproval[]>([]);
  const scrollRef = useRef<HTMLDivElement>(null);

  const loadPending = async () => {
    try {
      const res = await api.get('/agent/approvals');
      setPending(res.data.pending ?? []);
    } catch {
      setPending([]);
    }
  };

  useEffect(() => {
    scrollRef.current?.scrollTo({ top: scrollRef.current.scrollHeight, behavior: 'smooth' });
  }, [history, loading]);

  useEffect(() => {
    loadPending();
    const t = window.setInterval(loadPending, 5000);
    return () => window.clearInterval(t);
  }, []);

  const resetConversation = () => {
    localStorage.removeItem(SESSION_KEY);
    setSessionId('');
    setHistory([]);
  };

  const decide = async (token: string, approved: boolean) => {
    try {
      await api.post(`/agent/approvals/${token}`, { approved });
    } catch {
      // ignore — the poll below will refresh the list
    }
    loadPending();
  };

  const sendMessage = async () => {
    const text = message.trim();
    if (!text || loading) return;

    setHistory(h => [...h, { role: 'user', content: text }]);
    setMessage('');
    setLoading(true);
    try {
      const res = await api.post('/agent/chat', { message: text, sessionId });
      if (res.data.sessionId) {
        setSessionId(res.data.sessionId);
        localStorage.setItem(SESSION_KEY, res.data.sessionId);
      }
      setHistory(h => [...h, { role: 'assistant', content: res.data.response }]);
    } catch {
      setHistory(h => [...h, { role: 'assistant', content: 'Sorry, something went wrong. Please try again.' }]);
    } finally {
      setLoading(false);
      loadPending();
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      sendMessage();
    }
  };

  return (
    <div className="mx-auto flex h-[70vh] max-w-2xl flex-col">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-primary">AI Agent</h1>
          <p className="mt-1 text-sm text-secondary">
            Ask about products, orders, or anything else.
            {sessionId && (
              <>
                {' '}
                — conversation resumes after reload.{' '}
                <button onClick={resetConversation} className="text-accent hover:underline">
                  Start a new conversation
                </button>
              </>
            )}
          </p>
        </div>
      </div>

      <div
        ref={scrollRef}
        className="card mt-4 flex-1 space-y-3 overflow-y-auto p-4"
        aria-live="polite"
      >
        {history.length === 0 && (
          <p className="text-sm text-secondary">Ask me something about the products or orders...</p>
        )}
        {history.map((m, i) => (
          <div key={i} className={`flex ${m.role === 'user' ? 'justify-end' : 'justify-start'}`}>
            <div
              className={`max-w-[80%] whitespace-pre-wrap rounded-2xl px-4 py-2 text-sm ${
                m.role === 'user'
                  ? 'bg-primary text-white'
                  : 'border border-muted bg-bg text-primary'
              }`}
            >
              {m.content}
            </div>
          </div>
        ))}
        {loading && (
          <div className="flex justify-start">
            <div className="rounded-2xl border border-muted bg-bg px-4 py-2 text-sm text-secondary">
              Thinking…
            </div>
          </div>
        )}
      </div>

      {pending.length > 0 && (
        <div className="mt-4 rounded-xl border border-accent/40 bg-surface p-3">
          <div className="text-sm font-semibold text-primary">
            Pending approvals ({pending.length})
          </div>
          <div className="mt-2 space-y-2">
            {pending.map(p => (
              <div key={p.token} className="flex items-center justify-between gap-2 text-sm">
                <span className="min-w-0 flex-1 truncate text-secondary" title={p.description}>
                  {p.description}
                </span>
                <button
                  onClick={() => decide(p.token, true)}
                  className="rounded-lg bg-primary px-3 py-1 text-xs font-medium text-white hover:opacity-90"
                >
                  Approve
                </button>
                <button
                  onClick={() => decide(p.token, false)}
                  className="rounded-lg border border-muted px-3 py-1 text-xs font-medium text-secondary hover:bg-bg"
                >
                  Deny
                </button>
              </div>
            ))}
          </div>
        </div>
      )}

      <div className="mt-4 flex gap-2">
        <input
          className="input-field"
          value={message}
          onChange={e => setMessage(e.target.value)}
          onKeyDown={handleKeyDown}
          placeholder="Type your message..."
          aria-label="Message"
        />
        <button onClick={sendMessage} disabled={loading || !message.trim()} className="btn-primary disabled:cursor-not-allowed disabled:opacity-60">
          Send
        </button>
      </div>
    </div>
  );
}