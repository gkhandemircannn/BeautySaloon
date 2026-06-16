import { useEffect, useMemo, useState } from 'react'
import { getBookedTimes } from '../services/bookingApi'
import { getBusinessSettings } from '../services/businessSettingsApi'

function DateTimePicker({ selectedDate, selectedTime, selectedSpecialist, selectedService, onSelectDate, onSelectTime }) {
  const [bookedTimes, setBookedTimes] = useState([])
  const [settings, setSettings] = useState(null)
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState('')
  const today = getDateId(new Date())

  useEffect(() => { if (!selectedDate) onSelectDate(today) }, [selectedDate, onSelectDate, today])
  useEffect(() => {
    getBusinessSettings().then(setSettings).catch(() => setError('Çalışma saatleri alınamadı.'))
  }, [])
  useEffect(() => {
    if (!selectedDate || !selectedSpecialist?.id) return
    setIsLoading(true); setError('')
    getBookedTimes(selectedSpecialist.id, selectedDate)
      .then(setBookedTimes).catch(() => setError('Uygun saatler alınamadı.')).finally(() => setIsLoading(false))
  }, [selectedDate, selectedSpecialist?.id])

  const slots = useMemo(() => settings ? createSlots(settings) : [], [settings])
  function getDateId(date) { return date.getFullYear() + '-' + String(date.getMonth() + 1).padStart(2, '0') + '-' + String(date.getDate()).padStart(2, '0') }
  function dateFromId(id) { return new Date(id + 'T00:00:00') }
  function minutes(time) { const [h, m] = time.slice(0, 5).split(':').map(Number); return h * 60 + m }
  function clock(value) { return String(Math.floor(value / 60)).padStart(2, '0') + ':' + String(value % 60).padStart(2, '0') }
  function createSlots(value) { const list=[]; for(let x=minutes(value.openingTime); x<=minutes(value.lastAppointmentTime); x+=value.slotIntervalMinutes) list.push(clock(x)); return list }
  function move(day) { const d=dateFromId(selectedDate || today); d.setDate(d.getDate()+day); const id=getDateId(d); if(id < today) return; onSelectDate(id); onSelectTime(null) }
  function isBooked(time) { const start=minutes(time), end=start+(selectedService?.duration || 30); return bookedTimes.some(b => start < minutes(b.appointmentTime)+b.durationMinutes && minutes(b.appointmentTime) < end) }
  function formatted() { return dateFromId(selectedDate || today).toLocaleDateString('tr-TR',{ weekday:'long', day:'numeric', month:'long', year:'numeric' }) }

  return (
    <section className="selection-section selection-section--narrow">
      <h2>Tarih ve Saat Seçin</h2><p>Uygun günü ve saati belirleyin.</p>
      <div className="date-box">
        <span className="field-label">Randevu Tarihi</span>
        <div className="date-nav">
          <button type="button" disabled={(selectedDate || today) === today} onClick={() => move(-1)}>‹</button>
          <div><strong>{formatted()}</strong><small>{(selectedDate || today) === today ? 'Bugün' : 'Seçili tarih'}</small></div>
          <button type="button" onClick={() => move(1)}>›</button>
        </div>
        <label className="calendar-input">Başka tarih seç<input type="date" min={today} value={selectedDate || today} onChange={(e) => { onSelectDate(e.target.value); onSelectTime(null) }} /></label>
      </div>
      <span className="field-label">Saat</span>
      {isLoading && <p className="info">Saatler kontrol ediliyor...</p>}
      {error && <p className="error-box">{error}</p>}
      <div className="time-grid">
        {slots.map(time => { const booked=isBooked(time); return <button key={time} type="button" disabled={booked || isLoading || Boolean(error)} className={`time-button ${booked ? 'time-button--booked' : ''} ${selectedTime === time ? 'time-button--selected' : ''}`} onClick={() => onSelectTime(time)}>{booked ? 'Dolu' : time}</button> })}
      </div>
    </section>
  )
}
export default DateTimePicker
