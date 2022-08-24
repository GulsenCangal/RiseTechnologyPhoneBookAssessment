using Contact.API.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.API.Constants;
using Report.API.Services.Interfaces;
using System.Text;

namespace Report.API.ServiceExtensions
{
    public static class RabbitMQConsumer
    {
        public static IApplicationBuilder UseRabbitMq(this IApplicationBuilder app)
        {
            var _reportSettings = app.ApplicationServices.GetRequiredService<IOptions<ReportSettings>>().Value;

            var conn = _reportSettings.rabbitMqConsumer;

            var createDocumentQueue = "create_document_queue";
            var documentCreateExchange = "document_create_exchange";

            ConnectionFactory connectionFactory = new()
            {
                Uri = new Uri(conn)
            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare(documentCreateExchange, "direct");

            channel.QueueDeclare(createDocumentQueue, false, false, false);
            channel.QueueBind(createDocumentQueue, documentCreateExchange, createDocumentQueue);

            var consumerEvent = new EventingBasicConsumer(channel);

            consumerEvent.Received += (ch, ea) =>
            {
                var reportService = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IReportService>();
                var incomingModel = JsonConvert.DeserializeObject<ReportRequestData>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                Console.WriteLine("Data received");
                Console.WriteLine($"Received Id: {incomingModel.reportId}");
                reportService.CreateReportDetail(incomingModel.reportId);
            };

            channel.BasicConsume(createDocumentQueue, true, consumerEvent);

            return app;
        }
    }
}
