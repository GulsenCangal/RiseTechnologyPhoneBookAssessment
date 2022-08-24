using Contact.API.Services.Interfaces;
using Contact.API.Services;
using Microsoft.EntityFrameworkCore;
using Report.API.Models.Context;
using Report.API.Services.Interfaces;
using Report.API.Services;
using Contact.API.Models.Context;
using Report.API.ServiceExtensions;
using Report.API.Constants;
using Report.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ReportDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection")));
builder.Services.Configure<ReportSettings>(builder.Configuration.GetSection("Options"));
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<ReportDbContext>().Database.Migrate();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRabbitMq();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
