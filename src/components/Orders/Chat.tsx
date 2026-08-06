import { useState } from 'react';
import api from '../../api/client';

export default function Chat() {
  const [message, setMessage] = useState('');
  const [response, setResponse] = useState('');
  const [sessionId, setSessionId] = useState('');

  const sendMessage = async () => {
    try {
      const res = await api.post('/agent/chat', { message, sessionId });
      setResponse(res.data.response);
      setSessionId(res.data.sessionId);
      setMessage('');
    } catch (error) {
      console.error('Chat error:', error);
    }
  };

  return (
    <div className="p-4 max-w-2xl mx-auto">
      <h2 className="text-2xl font-bold mb-4">AI Agent</h2>
      <div className="border p-4 mb-4 min-h-[100px] bg-gray-50 rounded whitespace-pre-wrap">
        {response || 'Ask me something about the products or orders...'}
      </div>
      <div className="flex gap-2">
        <input
          className="flex-1 border p-2 rounded"
          value={message}
          onChange={e => setMessage(e.target.value)}
          placeholder="Type your message..."
          onKeyDown={e => e.key === 'Enter' && sendMessage()}
        />
        <button onClick={sendMessage} className="bg-blue-600 text-white p-2 rounded">Send</button>
      </div>
    </div>
  );
}