# FormsApp

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş temel bir ürün ve kategori yönetim uygulamasıdır.

## Özellikler

- Ürünleri listeleme, ekleme, düzenleme ve silme
- Kategoriye göre filtreleme ve arama
- Ürün görsellerini yükleme ve görüntüleme

## Proje Yapısı

- `Controllers/` — MVC Controller dosyaları
- `Models/` — Veri modelleri ve repository
- `Views/` — Razor View dosyaları
- `wwwroot/` — Statik dosyalar (CSS, JS, görseller)
- `Program.cs` — Uygulama başlangıç noktası

## Kurulum

1. Bu repoyu klonlayın:
   ```sh
   git clone <repo-url>
   cd FormsApp
   ```

2. Gerekli NuGet paketlerini yükleyin:
   ```sh
   dotnet restore
   ```

3. Uygulamayı başlatın:
   ```sh
   dotnet run
   ```

4. Tarayıcınızda `http://localhost:5000` adresini ziyaret edin.

## Notlar

- Ürün görselleri `wwwroot/img` klasörüne kaydedilir.
- Varsayılan ürün ve kategoriler `Models/Repository.cs` dosyasında tanımlanmıştır.

## Katkı

Katkıda bulunmak için lütfen bir pull request gönderin.

## Lisans

MIT
