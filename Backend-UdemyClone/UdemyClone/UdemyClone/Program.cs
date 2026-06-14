using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models;
using UdemyClone.Application.Services;
using UdemyClone.Domain.Database;
using UdemyClone.Domain.Interfaces;
using UdemyClone.Infrastructure.Email;
using UdemyClone.Infrastructure.Repositories;
using UdemyClone.WebApi.Middlewares;

// ─── SERILOG ───────────────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.MSSqlServer(
    connectionString: "Server=localhost,1433;Database=UdemyClone;User Id=sa;Password=Admin1234@;TrustServerCertificate=True;",
    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
    {
        TableName = "Logs",
        AutoCreateSqlTable = true
    })
    .CreateLogger();

// ─── BUILDER ───────────────────────────────────────────────
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// ─── BASE DE DATOS ─────────────────────────────────────────
builder.Services.AddDbContext<UdemyCloneContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ─── EMAIL ─────────────────────────────────────────────────
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

// ─── SERVICIOS ─────────────────────────────────────────────
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<ISeccionService, SeccionService>();
builder.Services.AddScoped<ILeccionService, LeccionService>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();
builder.Services.AddScoped<IResenaService, ResenaService>();
builder.Services.AddScoped<IListaDeseoService, ListaDeseoService>();
builder.Services.AddScoped<IProgresoService, ProgresoService>();

// ─── REPOSITORIOS ──────────────────────────────────────────
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ISeccionRepository, SeccionRepository>();
builder.Services.AddScoped<ILeccionRepository, LeccionRepository>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IResenaRepository, ResenaRepository>();
builder.Services.AddScoped<IListaDeseoRepository, ListaDeseoRepository>();
builder.Services.AddScoped<IProgresoRepository, ProgresoRepository>();

// ─── JWT ───────────────────────────────────────────────────
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ─── CONTROLLERS ───────────────────────────────────────────
builder.Services.AddControllers();

// ─── OPENAPI + SCALAR ──────────────────────────────────────
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info = new()
        {
            Title = "Tech Academy API",
            Version = "v1",
            Description = "API REST completa para la plataforma Tech Academy. " +
                         "Desarrollada con .NET 9, Entity Framework Core y JWT.",
            Contact = new OpenApiContact
            {
                Name = "Jenny Vaque",
                Email = "info@techacademy.com"
            }
        };
        return Task.CompletedTask;
    });

    // Agregar seguridad JWT en Scalar
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

// ─── CORS ──────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ─── APP ───────────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Scalar — reemplaza Swagger con UI moderna
    app.MapScalarApiReference(options =>
    {
        options.Title = " Tech Academy API";
        options.Theme = ScalarTheme.Purple;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);

    });
}

app.UseCors("AllowAngular");
app.UseSerilogRequestLogging();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();