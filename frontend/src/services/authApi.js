const API_URL = import.meta.env.VITE_API_URL

export async function loginAdmin(email, password) {
  const response = await fetch(`${API_URL}/api/auth/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    credentials: 'include',
    body: JSON.stringify({ email, password }),
  })
  if (!response.ok) throw new Error('E-posta adresi veya şifre hatalı.')
  return response.json()
}

export async function getCurrentAdmin() {
  const response = await fetch(`${API_URL}/api/auth/me`, { credentials: 'include' })
  if (!response.ok) return null
  return response.json()
}

export async function logoutAdmin() {
  const response = await fetch(`${API_URL}/api/auth/logout`, {
    method: 'POST',
    credentials: 'include',
  })
  if (!response.ok) throw new Error('Çıkış yapılamadı.')
  return response.json()
}
