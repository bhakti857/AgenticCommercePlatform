import { useEffect, useRef, useState } from 'react';
import api from '../../api/client';

interface ChatMessage {
  role: 'user' | 'assistant';
  content: string;
}

export default function Chat() {
  const [message, setMessage] = useState('');
  const [history, setHistory] = useState<ChatMessage[]>([]);
  const [sessionId, setSessionId] = useState('');
  const [loading, setLoading] = useState(false);
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    scrollRef.current?.scrollTo({ top: scrollRef.current.scrollHeight, behavior: 'smooth' });
  }, [history, loading]);

  const sendMessage = async () => {
    const text = message.trim();
    if (!text || loading) return;

    setHistory(h => [...h, { role: 'user', content: text }]);
    setMessage('');
    setLoading(true);
    try {
      const res = await api.post('/agent/chat', { message: text, sessionId });
      setSessionId(res.data.sessionId);
      setHistory(h => [...h, { role: 'assistant', content: res.data.response }]);
    } catch {
      setHistory(h => [...h, { role: 'assistant', content: 'Sorry, something went wrong. Please try again.' }]);
    } finally {
      setLoading(false);
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
      <h1 className="text-2xl font-bold text-primary">AI Agent</h1>
      <p className="mt-1 text-sm text-secondary">Ask about products, orders, or anything else.</p>

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
