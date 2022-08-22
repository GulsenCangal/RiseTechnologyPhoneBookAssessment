using Contact.API.Models.Context;
using Contact.API.Services.Interfaces;
using Contact.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ContactDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection")));
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IContactInformationService, ContactInformationService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<ContactDbContext>().Database.Migrate();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
