using Microsoft.EntityFrameworkCore;
using VariacaoAtivo.API.Configurations;
using VariacaoAtivo.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("SqlConnectionString");
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();

// Create APP
var app = builder.Build();

// Executa migrations pendentes
app.Services.GetService<Context>()?.Database.Migrate();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
