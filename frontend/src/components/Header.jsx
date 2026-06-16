function Header({ onStartBooking }) {
  return (
    <header className="header">
      <div className="header__inner">
        <button type="button" className="header__brand" onClick={() => window.location.reload()}>
          <span className="header__logo">✦</span>
          <span className="header__brand-text">
            <strong>Lumi Beauty</strong>
            <small>Beauty Studio</small>
          </span>
        </button>
        <button type="button" className="header__booking-button" onClick={onStartBooking}>
          Randevu Al
        </button>
      </div>
    </header>
  )
}
export default Header
