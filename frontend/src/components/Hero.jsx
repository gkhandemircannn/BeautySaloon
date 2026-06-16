function Hero({ onStartBooking }) {
  return (
    <section className="hero">
      <p className="hero__eyebrow">Lumi Beauty Studio</p>
      <h1 className="hero__title">Kendine ayırdığın zamanı güzelleştir.</h1>
      <p className="hero__description">
        Hizmetini seç, uzmanını belirle ve uygun saate randevunu kolayca oluştur.
      </p>
      <button type="button" className="hero__button" onClick={onStartBooking}>
        Randevu Al <span>→</span>
      </button>
    </section>
  )
}
export default Hero
