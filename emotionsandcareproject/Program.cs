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
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PacienteRepository>();
builder.Services.AddScoped<ISpecialistService, EspecialistaService>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<IDateService, DateService>();
builder.Services.AddScoped<IDateRepository, DateRepository>();
builder.Services.AddScoped<IQuestionnaireService, CuestionarioService>();
builder.Services.AddScoped<IQuestionnaireRepository, CuestionarioRepository>();
builder.Services.AddScoped<ITermsService, TermsService>();
builder.Services.AddScoped<ITermsAndConditionsRepository, TerminosYCondicionesRepository>();
builder.Services.AddScoped<INoteService, NotaService>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IEmotionSerrvice, EmotionService>();
builder.Services.AddScoped<IEmotionRepository, EmotionRepository>();
builder.Services.AddScoped<INotificationService, NotificacionService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IRecomendationRepository, RecomendationRepository>();
builder.Services.AddScoped<IRecomendationService, RecomendacionService>();
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
builder.Services.AddScoped<IItemsService, ItemsSerice>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IGoalService, LogroService>();
builder.Services.AddScoped<IStageService, StageService>();
builder.Services.AddScoped<IStagesRepository, StageRepository>();
builder.Services.AddSingleton<PushNotificationService>();
builder.Services.AddScoped<IOppsService, OppsService>();
builder.Services.AddScoped<IOppsRepository, OppsRepository>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<ITextRepository, TextRepository>();

builder.Services.AddScoped<IBadgeService, BadgeService>();
builder.Services.AddScoped<IBadgeRepository, BadgeRepository>();

builder.Services.AddScoped<IDummyUserRepository, DummyUserRepository>();
builder.Services.AddScoped<IDummyUserService, DummyUserService>();

builder.Services.AddScoped<IDBRepository, DBReporitory>();


var app = builder.Build();
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(
        @"{
          ""type"": ""service_account"",
          ""project_id"": ""emotionsandcareal"",
          ""private_key_id"": ""07dfd5343d9b797cb2eda870c7817c2f3210bf0b"",
          ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCUBtQ2uT5UnYwd\nJf+ArlSXS0d4GqB1aqk4xCTUj48GHGXx89p78+9gi7RTLjUa2IUsaDDeb+5V7QSZ\n7a4YUZxWH9rHTSci0LRTSLABaDr5l8eKan+/c7fxpvfxW6tv7RvpywzOLaMUG7or\ntlv+NYGwQWA79sjp48bUeR5qjsciQfJuvuMAibJ7s8BCHk+kHEufREK3L9DQy4Ri\nLbLj4MGXwKKR5u+lRSWDAzADCqaenTMegwxdMwUx0OUzMTHj3Jhsubc5CAAr5nKX\nEEBUB4R8vH22ryRoA0lXWkEJGXkPGVUPtuXrA1xqckznwSorcCuq/8kzxN2CUeTi\nf8RDFXFBAgMBAAECggEAA0y2DRw7kLj2/5STwg+Xn9iaMR9C00/KtrFYbLvmlqGw\nVFqB/4BAgb7j3h9IlmHGxsIWYrDfPcD1HVLoRmke7NtPu7XU1GG6FHRUCD/OqtUg\nkaLoD0kd5zbjDhAOgjzA0Tni8dTEpa8H4HvnYEA2kHVFwbpWKUkeFa+AoTyLS/gL\nJJKBxbc+oiZV95/gRh8Ua0nk3hqLSM6sk2YWMVZn8b6VQlCZezUcFDla+RMyX3q9\nnW5nRq9qWbkV+3orQRK+7NBcIKGMnwkFAEWVBQQUEIN23jucriaK65SlW7dUAK+Z\n/k7S1Kjdp93aGuN55VrDsWqMy5Ta1mqB16pF32+H0wKBgQC2+mNqNNwQrPe/v1VP\nYxsaZwR45BZhYVSUoTebO0ElIsz5lprR3vB43O5a6ErAJaKqSJ85ADQbvDecfIcS\nrVEPvsgVzVWE3slEPpStHV0RrJKaebJEOQtWZ/ukg7xwmdVyIGEJ08FE01l7esea\nE6DmaaPTtKXCeYH9S0KifvdnFwKBgQDPGbI1ODJmeq4t9EdEvRGWJD8cMMD7MR73\noo3l7PPiTuZXayGpSvXvDQcbzLf6VMBhj4ma3xaFW7ZiHJ8jLiI5q62uro58FVSm\nTXL3cs4OuW6Z55ezN+u5ZwP1PWzezKChX+tlVEXaw2FjQn/E9fG1bv64lCpTm/nA\n4iHdCWchZwKBgQCWMoqQj7tk5NapS0GX3N5OmemN4oyMevyW1I90mPsspJhk+D81\nry7tx0zQyoUxnLMd3Gb2vzgG3EU56u5lYmd76TsMynQ8kTPdFRGt2MCg1Wux6dtv\nLPQlhU2HCawRMfWHrRR6oJuxB1wYg/x8eUhGWsjj6xF1xY/yf2i/QkGLGQKBgCAC\nF7H0Ao4mLd96Xr13/0zWQ07HFjx8hg29+PJtYfA7Q4yFSUmSBVqyB8Q7KsAs8yLg\n/WKLUj7AQvr883eLfMyvBQP3hJwW6+NcGwW1n7VU4Yw16BNR9EPOcSUpHjd9JuJq\nIcaidL1v7xBZScgWPwpMol/Cvpv3gm1WReeUuXTHAoGBAJ4puUjvYFnQeLYVJHaU\nEf7dCtxmZFf3mZfaLjPm0J47AzDXaxmakFfTDsz0+UlrlYgJQUkmGtcfAMtriD1P\ncztodAvzZQylKZ/tXZdcYvXlcnWKwNtlkL02zqod5bSGxffvBNYSuLxL/nL+kedE\nlFr78os8GXt5X8GVfbD6JWjw\n-----END PRIVATE KEY-----\n"",
          ""client_email"": ""firebase-adminsdk-aef5s@emotionsandcareal.iam.gserviceaccount.com"",
          ""client_id"": ""109090773728153847853"",
          ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
          ""token_uri"": ""https://oauth2.googleapis.com/token"",
          ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
          ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-aef5s%40emotionsandcareal.iam.gserviceaccount.com"",
          ""universe_domain"": ""googleapis.com""
        }")
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