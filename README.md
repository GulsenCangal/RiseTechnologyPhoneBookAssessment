# RiseTechnologyPhoneBookAssessment
Rise Tecnology için PhoneBook projesi oluşturuldu.
<h2>Phone Book Uygulaması</h2>
Kişiler ile ilgili crud işlemlerin yapılıp kişilerin lokasyon verilerine göre rapor oluşturabilecek ve bu raporu bir yola csv olarak doküman halinde çıktısını oluşturacak bir uygulamadır. Kişiler, iletişim bilgileri ve raporlara ait kayıtların tutulduğu database'i EntityFrameWork ve Code First yapısı kullanılarak oluşturulmaktadır. Kişiler ve iletişim bilgilerinin crud işlemlerinin yapıldığı microservice ile rapor crud işlemlerinin yapıldığı microservice'lerinin haberleştirilmektedir. Async metodlar ile birlikte gelecek rapor taleplerini dinleyip bu talepleri kuyruğa iletecek RabbitMQ yapısı oluşturuldu. Microserviceler için docker ayarları yapılarak geliştiricilerin farklı işletim sistemlerinde ekledikleri diğer eklenti yazılımlar ile uyumlu bir şekilde çalışması planlanmıştır. 
<h2>Phone Book Kullanılan Teknolojiler</h2>
<ul>
  <li>.NET 6</li>
  <li>Entity Framework Core 6</li>
  <li>Docker</li>
    <li>RabbitMQ</li>
    <li>xUnit</li>
    <li>Moq</li>
    <li>Coverlet</li>
</ul>  
<h2>Phone Book Gereklilikler</h2>
Docker'ın bilgisayarınızda yüklü olduğundan emin olunuz. Ardından aşağıdaki komut ile RabbitMq'u Docker üzerinden çalıştırınız.
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
PostgreSQL'nin bilgisayarınızda yüklü olduğundan emin olunuz. Ardından aşağıdaki adımları uygulayınız.

Coverlot için öncelikli olarak Nuget console'dan:<br>
*cd test klasör yolu<br>
*dotnet add package coverlet.msbuild<br>
*dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura<br>
adımlarını izleyiniz.

PhoneBook.API içerisinde bulunan appsettings.json dosyasındaki "ConnectionStrings" içerisinde bulunan User ID, Password ve Host bilgilerini kendinize uygun şekilde düzenleyeniz.
Report.API içerisinde bulunan appsettings.json dosyasındaki "ConnectionStrings" içerisinde bulunan User ID, Password ve Host bilgilerini kendinize uygun şekilde düzenleyeniz.
RabbitMQ bağlantı bilgisini kendinize göre düzenlemek isterseniz eğer aşağıdaki adımları uygulayınız.

PhoneBook.API içerisinde bulunan appsettings.json dosyasındaki "Options" içerisinde bulunan RabbitMqCon bilgisini kendinize uygun şekilde düzenleyeniz.

Report.API içerisinde bulunan appsettings.json dosyasındaki "Options" içerisinde bulunan RabbitMqCon bilgisini kendinize uygun şekilde düzenleyeniz.

PhoneBook.API için Report.API içerisinde bulunan appsettings.json dosyasındaki PhoneBookApiUrl bilgisini kendinize uygun şekilde düzenleyiniz.

Report.API için PhoneBook.API içerisinde bulunan appsettings.json dosyasındaki ReportApiUrl bilgisini kendinize uygun şekilde düzenleyiniz.

Projeler varsayılan ayarlar ile derlenip, çalıştırıldığında aşağıdaki url'ler üzerinden swagger arayüzüne ulaşabilirsiniz.

PhoneBook.API Url: https://localhost:7271/swagger/index.html
Report.API Url   : https://localhost:7055/swagger/index.html

<h2>Unit Test Code Coverage Sonuçları</h2>

![unitTtestCoverage](https://user-images.githubusercontent.com/111676187/186534641-1b4538cc-8cb5-4fc7-bdb4-81ac406b07bd.PNG)
