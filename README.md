# Lumi Beauty Studio
Güzellik salonu randevu otomasyonu.

## Yeni sürüm özellikleri
- Hizmete göre uygun uzman filtreleme: tırnak, saç, cilt, vücut, kaş-kirpik ve kalıcı makyaj ayrı uzmanlarla çalışır.
- Admin panelinden uzman ekleme, düzenleme ve kaldırma.
- Uzman bazlı dolu saat kontrolü.
- Çalışma saatleri, onay bekleyen randevular, SMS test çıktısı ve tarih aralığı raporları.

## İlk kurulum
```bash
cd ~/Projects/LumiBeautySalon/backend/LumiBeauty.Api
dotnet restore
dotnet user-secrets init
dotnet user-secrets set "AdminSeed:Email" "admin@lumibeauty.com"
dotnet user-secrets set "AdminSeed:Password" "KENDI_GUCLU_SIFREN"
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
Ayrı terminal:
```bash
cd ~/Projects/LumiBeautySalon/frontend
npm install
npm run dev
```
Müşteri: http://localhost:5174  Admin: http://localhost:5174/admin

## Eski sürümü kurduysan
Backend klasöründe:
```bash
dotnet ef migrations add AddSpecialistManagement
dotnet ef database update
dotnet run
```
