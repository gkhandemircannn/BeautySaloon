function BookingSummary({ booking, onRestart }) {
  return <section className="summary"><div className="summary__icon">⏳</div><h2>Randevu Talebiniz Alındı</h2><p>Randevunuz işletme tarafından kontrol edilecek. Onaylandığında telefon numaranıza bilgilendirme mesajı gönderilecek.</p><div className="summary__card">
    <div><span>Hizmet</span><strong>{booking.serviceName}</strong></div><div><span>Uzman</span><strong>{booking.specialistName}</strong></div><div><span>Tarih</span><strong>{booking.appointmentDate}</strong></div><div><span>Saat</span><strong>{booking.appointmentTime.slice(0,5)}</strong></div><div><span>Durum</span><strong>Onay Bekliyor</strong></div>
  </div><button onClick={onRestart}>Yeni Randevu Oluştur</button></section>
}
export default BookingSummary
