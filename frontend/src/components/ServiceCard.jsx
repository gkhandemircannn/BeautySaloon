function ServiceCard({ service, selected, onSelect }) {
  return (
    <article className={`service-card ${selected ? 'card--selected' : ''}`}>
      <div>
        <h3>{service.name}</h3>
        <p className="card__price">{service.price} TL</p>
        <p className="card__muted">{service.duration} dakika</p>
      </div>
      <button type="button" className={`choice-button ${selected ? 'choice-button--selected' : ''}`} onClick={() => onSelect(service)}>
        {selected ? 'Seçildi' : 'Seç'}
      </button>
    </article>
  )
}
export default ServiceCard
