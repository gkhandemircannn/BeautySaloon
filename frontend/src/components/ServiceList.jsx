import { services } from '../data/services'
import ServiceCard from './ServiceCard'

function ServiceList({ selectedService, onSelectService }) {
  return (
    <section className="selection-section">
      <h2>Hizmetinizi Seçin</h2>
      <p>Size uygun bakım hizmetini belirleyin.</p>
      <div className="selection-grid">
        {services.map((service) => (
          <ServiceCard key={service.id} service={service} selected={selectedService?.id === service.id} onSelect={onSelectService} />
        ))}
      </div>
    </section>
  )
}
export default ServiceList
