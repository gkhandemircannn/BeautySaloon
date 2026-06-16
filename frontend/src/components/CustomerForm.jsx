import { useState } from 'react'
function CustomerForm({ onSubmit, onBack, isSubmitting, submitError }) {
  const [fullName,setFullName]=useState(''); const [phone,setPhone]=useState(''); const [note,setNote]=useState(''); const [errors,setErrors]=useState({})
  async function handleSubmit(e){e.preventDefault(); const next={}; if(fullName.trim().length<3) next.fullName='Lütfen adınızı ve soyadınızı girin.'; if(phone.replace(/\D/g,'').length<10) next.phone='Geçerli bir telefon numarası girin.'; setErrors(next); if(Object.keys(next).length) return; await onSubmit({fullName:fullName.trim(),phone:phone.trim(),note:note.trim()})}
  return <section className="selection-section selection-section--narrow"><h2>Bilgilerinizi Girin</h2><p>Onay mesajı için iletişim bilgilerinizi yazın.</p><form className="customer-form" onSubmit={handleSubmit}>
    <label>Ad Soyad<input value={fullName} onChange={e=>setFullName(e.target.value)} placeholder="Örnek: Ayşe Yılmaz" />{errors.fullName&&<small>{errors.fullName}</small>}</label>
    <label>Telefon<input value={phone} onChange={e=>setPhone(e.target.value)} placeholder="05XX XXX XX XX" />{errors.phone&&<small>{errors.phone}</small>}</label>
    <label>Not <em>(isteğe bağlı)</em><textarea value={note} onChange={e=>setNote(e.target.value)} placeholder="Eklemek istediğiniz not" /></label>
    {submitError&&<p className="error-box">{submitError}</p>}
    <div className="form-actions"><button type="button" className="secondary-button" onClick={onBack}>Geri</button><button disabled={isSubmitting}>{isSubmitting?'Oluşturuluyor...':'Randevu Talebi Oluştur'}</button></div>
  </form></section>
}
export default CustomerForm
