const API_URL = import.meta.env.VITE_API_URL

async function handleAdminResponse(response, fallback) {
  if (response.status === 401) throw new Error('UNAUTHORIZED')
  if (!response.ok) {
    const message = await response.text()
    throw new Error(message || fallback)
  }
  return response.json()
}

export async function getBookings() {
  const response = await fetch(`${API_URL}/api/bookings`, { credentials: 'include' })
  return handleAdminResponse(response, 'Randevular alınamadı.')
}

export async function confirmBooking(id) {
  const response = await fetch(`${API_URL}/api/bookings/${id}/confirm`, {
    method: 'PATCH', credentials: 'include',
  })
  return handleAdminResponse(response, 'Randevu onaylanamadı.')
}

export async function cancelBooking(id) {
  const response = await fetch(`${API_URL}/api/bookings/${id}/cancel`, {
    method: 'PATCH', credentials: 'include',
  })
  return handleAdminResponse(response, 'Randevu iptal edilemedi.')
}
