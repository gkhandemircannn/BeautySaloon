const API_URL = import.meta.env.VITE_API_URL

export async function getBusinessSettings() {
  const response = await fetch(`${API_URL}/api/business-settings`)
  if (!response.ok) throw new Error('Çalışma saatleri alınamadı.')
  return response.json()
}

export async function updateBusinessSettings(settings) {
  const response = await fetch(`${API_URL}/api/business-settings`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    credentials: 'include',
    body: JSON.stringify(settings),
  })

  if (response.status === 401) throw new Error('UNAUTHORIZED')
  if (!response.ok) {
    const message = await response.text()
    throw new Error(message || 'Çalışma saatleri güncellenemedi.')
  }
  return response.json()
}
