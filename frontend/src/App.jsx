import { useState } from 'react'
import Header from './components/Header'
import Hero from './components/Hero'
import ServiceList from './components/ServiceList'
import SpecialistList from './components/SpecialistList'
import DateTimePicker from './components/DateTimePicker'
import CustomerForm from './components/CustomerForm'
import BookingSummary from './components/BookingSummary'
import { createBooking } from './services/bookingApi'
import './App.css'

function App(){
 const [step,setStep]=useState('home'); const [service,setService]=useState(null); const [specialist,setSpecialist]=useState(null); const [date,setDate]=useState(null); const [time,setTime]=useState(null); const [created,setCreated]=useState(null); const [submitting,setSubmitting]=useState(false); const [error,setError]=useState('')
 function reset(){setStep('home');setService(null);setSpecialist(null);setDate(null);setTime(null);setCreated(null);setError('')}
 function selectService(x){setService(x);setSpecialist(null);setDate(null);setTime(null)}
 function next(){if(step==='service'&&service)setStep('specialist'); else if(step==='specialist'&&specialist)setStep('datetime'); else if(step==='datetime'&&date&&time)setStep('customer')}
 function back(){if(step==='service')setStep('home'); else if(step==='specialist'){setStep('service');setSpecialist(null)} else if(step==='datetime'){setStep('specialist');setDate(null);setTime(null)} else if(step==='customer')setStep('datetime')}
 async function submit(form){setSubmitting(true);setError('');try{const booking=await createBooking({serviceName:service.name,servicePrice:service.price,durationMinutes:service.duration,serviceCategory:service.category,specialistId:specialist.id,appointmentDate:date,appointmentTime:time+':00',customerName:form.fullName,customerPhone:form.phone,customerNote:form.note||null});setCreated(booking);setStep('summary')}catch(e){setError(e.message)}finally{setSubmitting(false)}}
 const showBar=['service','specialist','datetime'].includes(step); const disabled=(step==='service'&&!service)||(step==='specialist'&&!specialist)||(step==='datetime'&&(!date||!time))
 return <div className="app"><Header onStartBooking={()=>setStep('service')} /><main>{step==='home'&&<Hero onStartBooking={()=>setStep('service')} />}{step==='service'&&<ServiceList selectedService={service} onSelectService={selectService}/>} {step==='specialist'&&<SpecialistList selectedService={service} selectedSpecialist={specialist} onSelectSpecialist={setSpecialist}/>} {step==='datetime'&&<DateTimePicker selectedDate={date} selectedTime={time} selectedSpecialist={specialist} selectedService={service} onSelectDate={setDate} onSelectTime={setTime}/>} {step==='customer'&&<CustomerForm onSubmit={submit} onBack={back} isSubmitting={submitting} submitError={error}/>} {step==='summary'&&<BookingSummary booking={created} onRestart={reset}/>}</main>{showBar&&<div className="bottom-bar"><button className="secondary-button" onClick={back}>Geri</button><button disabled={disabled} onClick={next}>Devam Et</button></div>}</div>
}
export default App
