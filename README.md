# RiseTechnologyPhoneBookAssessment
Rise Tecnology için PhoneBook projesi oluşturuldu.
<h2>Phone Book Uygulaması</h2>
Kişiler ile ilgili crud işlemlerin yapılıp kişilerin lokasyon verilerine göre rapor oluşturabilinecek bir uygulamadır. Kişiler, iletişim bilgileri ve raporlara ait kayıtların tutulduğu database'i EntityFrameWork ve Code First yapısı kullanılarak oluşturulmaktadır. Kişiler ve iletişim bilgilerinin crud işlemlerinin yapıldığı microservice ile rapor crud işlemlerinin yapıldığı microservice'lerinin haberleştirilmektedir. Async metodlar ile birlikte gelecek rapor taleplerini dinleyip bu talepleri kuyruğa iletecek RabbitMQ yapısı oluşturuldu. Microserviceler için docker ayarları yapılarak geliştiricilerin farklı işletim sistemlerinde ekledikleri diğer eklenti yazılımlar ile uyumlu bir şekilde çalışması planlanmıştır.
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
<h2>Phone Book Gerk</h2>
