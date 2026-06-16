const API_URL = import.meta.env.VITE_API_URL
async function read(response){ if(response.status===401) throw new Error('UNAUTHORIZED'); if(!response.ok) throw new Error((await response.text()) || 'İşlem tamamlanamadı.'); return response.json() }
export async function getSpecialists(category){ const q=new URLSearchParams(); if(category) q.set('category',category); return read(await fetch(`${API_URL}/api/specialists?${q}`)) }
export async function getAdminSpecialists(){ return read(await fetch(`${API_URL}/api/specialists/admin`,{credentials:'include'})) }
export async function createSpecialist(payload){ return read(await fetch(`${API_URL}/api/specialists`,{method:'POST',headers:{'Content-Type':'application/json'},credentials:'include',body:JSON.stringify(payload)})) }
export async function updateSpecialist(id,payload){ return read(await fetch(`${API_URL}/api/specialists/${id}`,{method:'PUT',headers:{'Content-Type':'application/json'},credentials:'include',body:JSON.stringify(payload)})) }
export async function removeSpecialist(id){ return read(await fetch(`${API_URL}/api/specialists/${id}`,{method:'DELETE',credentials:'include'})) }
