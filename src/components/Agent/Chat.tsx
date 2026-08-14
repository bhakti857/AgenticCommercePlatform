import { useState } from 'react';
import api from '../../api/client';

interface ChatMessage {
    role: 'user' | 'assistant';
    content: string;
}

export default function Chat() {
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState<ChatMessage[]>([]);
    const [sessionId, setSessionId] = useState<string | undefined>(undefined);
    const [loading, setLoading] = useState(false);

    const sendMessage = async () => {
        if (!message.trim() || loading) return;

        const userMessage = message;
        setMessages(prev => [...prev, { role: 'user', content: userMessage }]);
        setMessage('');
        setLoading(true);

        try {
            const res = await api.post('/agent/chat', {
                message: userMessage,
                sessionId,
            });
            setMessages(prev => [...prev, { role: 'assistant', content: res.data.response }]);
            setSessionId(res.data.sessionId);
        } catch (err) {
            setMessages(prev => [...prev, { role: 'assistant', content: '⚠️ Error reaching the agent. Please try again.' }]);
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === 'Enter') sendMessage();
    };

    return (
        <div className="p-4 max-w-2xl mx-auto">
            <h2 className="text-2xl font-bold mb-4">AI Agent</h2>
            <div className="border p-4 mb-4 min-h-[200px] max-h-[400px] overflow-y-auto bg-gray-50 rounded space-y-2">
                {messages.length === 0 && (
                    <p className="text-gray-400">Ask me something about the products, orders, or the codebase...</p>
                )}
                {messages.map((m, i) => (
                    <div
                        key={i}
                        className={`p-2 rounded max-w-[80%] whitespace-pre-wrap ${m.role === 'user'
                                ? 'bg-blue-600 text-white ml-auto'
                                : 'bg-white border'
                            }`}
                    >
                        {m.content}
                    </div>
                ))}
                {loading && <div className="text-gray-400 italic">Thinking...</div>}
            </div>
            <div className="flex gap-2">
                <input
                    className="flex-1 border p-2 rounded"
                    value={message}
                    onChange={e => setMessage(e.target.value)}
                    onKeyDown={handleKeyDown}
                    placeholder="Type your message..."
                    disabled={loading}
                />
                <button
                    onClick={sendMessage}
                    className="bg-blue-600 text-white p-2 rounded disabled:opacity-50"
                    disabled={loading}
                >
                    Send
                </button>
            </div>
        </div>
    );
}