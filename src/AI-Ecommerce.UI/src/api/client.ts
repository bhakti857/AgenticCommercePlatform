import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5015/api',
  headers: { 'Content-Type': 'application/json' },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// The API serializes with System.Text.Json ReferenceHandler.Preserve, which wraps
// every array as {"$id":"1","$values":[...]} and can emit {"$ref":"n"} back-references.
// Recursively unwrap those so the rest of the UI can treat response.data as plain
// arrays/objects.
function unwrap(node: unknown, seen: Map<string, unknown>): unknown {
  if (Array.isArray(node)) return node.map(n => unwrap(n, seen));
  if (node === null || typeof node !== 'object') return node;

  const obj = node as Record<string, unknown>;
  if (typeof obj.$ref === 'string') return seen.get(obj.$ref) ?? node;
  if (Array.isArray(obj.$values)) {
    const arr = obj.$values.map(v => unwrap(v, seen));
    if (typeof obj.$id === 'string') seen.set(obj.$id, arr);
    return arr;
  }

  const result: Record<string, unknown> = {};
  if (typeof obj.$id === 'string') seen.set(obj.$id, result);
  for (const key of Object.keys(obj)) {
    if (key === '$id') continue;
    result[key] = unwrap(obj[key], seen);
  }
  return result;
}

api.interceptors.response.use((response) => {
  response.data = unwrap(response.data, new Map());
  return response;
});

function isRetryableAuthError(error: unknown): boolean {
  return (
    axios.isAxiosError(error) &&
    error.response?.status === 401 &&
    !String(error.config?.url).startsWith('/auth/') &&
    Boolean(localStorage.getItem('refreshToken'))
  );
}

async function refreshToken(): Promise<string | null> {
  const refresh = localStorage.getItem('refreshToken');
  if (!refresh) return null;

  const res = await axios.post('http://localhost:5015/api/auth/refresh', {
    refreshToken: refresh,
  });
  localStorage.setItem('token', res.data.token);
  if (res.data.refreshToken) localStorage.setItem('refreshToken', res.data.refreshToken);
  const { token: _t, refreshToken: _r, ...userData } = res.data;
  localStorage.setItem('user', JSON.stringify(userData));
  return res.data.token;
}

let refreshing: Promise<string | null> | null = null;

api.interceptors.response.use(undefined, async (error) => {
  const original = error?.config;
  if (!isRetryableAuthError(error) || original?._retried) return Promise.reject(error);

  original._retried = true;
  refreshing = refreshing ?? refreshToken().finally(() => { refreshing = null; });
  const newToken = await refreshing;

  if (newToken) {
    original.headers = original.headers ?? {};
    original.headers.Authorization = `Bearer ${newToken}`;
    return api(original);
  }

  localStorage.removeItem('token');
  localStorage.removeItem('refreshToken');
  return Promise.reject(error);
});

export default api;