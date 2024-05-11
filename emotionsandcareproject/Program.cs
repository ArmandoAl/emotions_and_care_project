using Business;
using Business.contracts;
using Business.Contracts;
using Business.implementations;
using Business.Implementations;
using Data.contracts;
using Data.Contracts;
using Data.implementations;
using Data.Implementations;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IEspecialistaService, EspecialistaService>();
builder.Services.AddScoped<IEspecialistaRepository, EspecialistaRepository>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<ICuestionarioService, CuestionarioService>();
builder.Services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();
builder.Services.AddScoped<ITerminosYCondicionesService, TerminosYCondicionesService>();
builder.Services.AddScoped<ITerminosYCondicionesRepository, TerminosYCondicionesRepository>();
builder.Services.AddScoped<INotaService, NotaService>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<IEmocionSerrvice, EmocionService>();
builder.Services.AddScoped<IEmocionRepository, EmocionRepository>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<ICartaRepository, CartaRapository>();
builder.Services.AddScoped<ICartaService, CartaService>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();
builder.Services.AddScoped<IPublicacionService, PublicacionService>();
builder.Services.AddScoped<IRecomendacionRepository, RecomendacionRepository>();
builder.Services.AddScoped<IRecomendacionService, RecomendacionService>();
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
builder.Services.AddScoped<IItemsService, ItemsSerice>();
builder.Services.AddScoped<ILogroRepository, LogroRepository>();
builder.Services.AddScoped<ILogroService, LogroService>();


var app = builder.Build();
FirebaseApp.Create(new AppOptions()
{

    Credential = GoogleCredential.FromJson(
        @"{
          ""type"": ""service_account"",
          ""project_id"": ""emotionsandcareal"",
          ""private_key_id"": ""7c5d2b9f04fdc1fd3a836510f43252dc1bdc3d9a"",
          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDqpUhuLLuYtCfv\nbNXxBgYn1v5CImB2WeW9g3XJElakfnzpIPiznLJTjgtNSE0DrwUD2VbOUzp3zkP3\nGAM3NtM4M4G7JxTumo66oz9C8EkWarn1jrYbMna2Zu2yTyDGb7lLrk/V6ijdcqM3\ndi68eT9kIrskaMlMyT8iTw9iR18cCSe2rDt4SQU+DTHOEYmCFvN8Me7oVPvK6Lu1\neu5hra0//TSpzZ9d9x0WhEiSO+23bY9C15B0rQuRkJ276WNjAg5Q/GagI6pQqnrP\nUTXd1HrrykUSp9NqXgdx4UhcD0dSuvX5VZ3cim7z7CqLZljYqIswl2ZnHshpW6tU\nEaLki+9JAgMBAAECggEAB6Xi5WIEeVBoVTfDn2TPxY1B4bXAiPFnX650I91UdDpO\nzioN2r9BDSBXnCmLoEYC0IkAeQTu2aHTYEkb+oXWC5wBXsn1NwCHP2xIiK3pS/EN\nmTd3r9o/7IEQi7Vj8mPVpjK+LzdzJO8MHkFck5ysfsVZ1GPf9jNAoT8l14SCncv0\nnWJtP9LweJDCIolWLRzX6Ju2FMxQc6ySbYh+miSmV2TTU5E3UCG98Z5LkAdSDPL3\nreIrqErtRDAL2Ucqbv3kssk/kxZH4PpfR2aZU8KEAwBkp175IDIHN6IrIIF9vmu7\ndss47L3EfPJ0YNvUU4IUUGzBGawuSr1xWEDOAOY7uwKBgQD5+3Iwa0hiK+UZsutD\nsIpaun5x6lkM7J1Ub+ViPqvk011uLReKIU70Xe5ipLImkHseQ1lyXc7W0hZTldMh\nSvzR9UQCSodTTr7XYhW9L6Kt6vMvblD18jOBgTgHB1kUy9UepHToyC+sgsUVcNFx\nNvQYmceXEBQAqOBb0wfyZ8nSQwKBgQDwS1KnusEn6wYbL8pHTHiwHsIQBz+muSnx\nS9NMkRDlrFNS9TW7VfYiA7Yga9II4/nI03SuTX67FHtgOKMcMSwvuqfSd6PSgBMh\n4MDOoD1PLSh58bUu5UQh6RCQ4wZkM3oUJkiohi8PiUjEh93zrvulBOqwJCiKvIvv\nUTnWDuFdgwKBgEdf28KnXwtorQkhZLr29Qnipaew2awvAtzQ9hWO/1VeZBbJGSd/\nSyPKjf3sOFF6fyys3iUhU6VSZr2G0bl3x6fK95gP34ORwDuO7dYOe8xcgQLR5JvU\ng5A+bNjU6EJf0IHnQtoUDkibLdppU+OXZSqA/dPL62okzapqHqK/r4gxAoGBAN3y\nZK0CJWjufxxbXvXOH2dlcZAGcfdX7fKvO8Lr9vR84BWu4etf8dDnJrD5he2qTMv+\n7DYW5Ch+OZrdlWLz1EFdoacX1JwYqhNPmicAAMECD/HanKRtJSpBMIjy+LNSjL7R\nwajPD+bEwg6tBAgRpuN8EB2TuFK05nesycP6yJRrAoGAOpBwDOWE+wbJL1XnyFut\nDTTRIRtl/Id63LDpwtEZG2BKCpNBmFNfYoDMF35tlf1Bm8QzUz4T8oyTVue4FQqU\nQjh/OYHjgjJtmeYdSlT4BuTHzX9fUg7iBVIavHvNWGR/g6keL8XIiZdJtbnGkQ7Q\nUNYFcVoKa3spQIoLP0y8T2s=\n-----END PRIVATE KEY-----\n"",
          ""client_email"": ""firebase-adminsdk-aef5s@emotionsandcareal.iam.gserviceaccount.com"",
          ""client_id"": ""109090773728153847853"",
          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
          ""token_uri"": ""https://oauth2.googleapis.com/token"",
          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-aef5s%40emotionsandcareal.iam.gserviceaccount.com"",
          ""universe_domain"": ""googleapis.com""
        }
        ")

});

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();