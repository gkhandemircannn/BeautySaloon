function SpecialistCard({ specialist, selected, onSelect }) {
  return (
    <article className={`specialist-card ${selected ? 'card--selected' : ''}`}>
      <div>
        <h3>{specialist.name}</h3>
        <p>{specialist.specialty}</p>
        <strong>★ {specialist.rating}</strong>
      </div>
      <button type="button" className={`choice-button ${selected ? 'choice-button--selected' : ''}`} onClick={() => onSelect(specialist)}>
        {selected ? 'Seçildi' : 'Seç'}
      </button>
    </article>
  )
}
export default SpecialistCard
