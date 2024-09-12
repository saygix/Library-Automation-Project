# Library Automation Project

## Proje Açıklaması

Bu proje, C# kullanılarak geliştirilen bir Windows Forms uygulamasıdır ve kütüphane yönetimini daha etkili ve kullanıcı dostu hale getirmeyi amaçlar. Proje, iki tür kullanıcı arayüzü sunar: Yönetici ve Kullanıcı.

## Özellikler

- **Yönetici ve Kullanıcı Giriş Ekranları**:
  - **Yönetici Girişi**: Yönetici, kütüphane kitaplarını ekleyebilir, yönetici kullanıcılarını yönetebilir ve kitap iade işlemlerini gerçekleştirebilir.
  - **Kullanıcı Girişi**: Kullanıcılar, sadece kitapları görüntüleyebilir, ödünç alabilir ve iade edebilir.

- **Kitap Yönetimi**:
  - Kitaplar sadece yönetici tarafından eklenebilir ve düzenlenebilir.
  - Yönetici tarafından eklenen kitaplar, kullanıcıların erişimine sunulur.

- **Kitap Ödünç Alma ve İade**:
  - Kullanıcılar kitapları ödünç alabilir.
  - Kitap iadeleri yönetici tarafından kontrol edilir ve işlenir.

- **Yönetici Yönetimi**:
  - Yönetici, diğer yöneticileri ekleyebilir ve mevcut yöneticileri yönetebilir.

- **SQL Bağlantısı**:
  - Veritabanı yönetimi ve SQL bağlantıları kullanılarak veri saklama işlemleri gerçekleştirilir.
  - Kitaplar, kullanıcılar ve yöneticiler hakkında bilgiler SQL veritabanında tutulur.

## Kullanılan Araçlar

- **C#**: Windows Forms uygulamasının temel programlama dili.
- **SQL Server**: Veritabanı yönetimi ve veri saklama.
- **Windows Forms**: Kullanıcı arayüzü oluşturma ve yönetme.

## Kurulum ve Kullanım

1. **Gereksinimler**:
   - Visual Studio (C# geliştirme ortamı)
   - SQL Server veya uyumlu bir veritabanı sistemi

2. **Kurulum**:
   - Projeyi klonlayın veya indirin.
   - Visual Studio'da projeyi açın.
   - SQL Server'da gerekli veritabanı ve tabloları oluşturun (örnek veritabanı yapısı proje ile birlikte sağlanabilir).
   - Projeyi çalıştırarak uygulamanın yönetici ve kullanıcı arayüzlerini test edin.

3. **Kullanım**:
   - Yönetici olarak giriş yaparak kitapları ekleyin ve yöneticileri yönetin.
   - Kullanıcı olarak giriş yaparak kitapları görüntüleyin, ödünç alın ve iade işlemlerini gerçekleştirin.


