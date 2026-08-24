using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Configuracoes;
using projetobiblioteca.Context;
using projetobiblioteca.FileExport.Exporter.Factory;
using projetobiblioteca.FileExport.Exporter.Impl;
using projetobiblioteca.HATEOAS.Filters;
using projetobiblioteca.Mail;
using projetobiblioteca.Repositorios;
using projetobiblioteca.Repositorios.Implementations;
using projetobiblioteca.Servicos;
using projetobiblioteca.Servicos.Implementation;
using projetobiblioteca.Tools.Bearer;
using projetobiblioteca.Tools.Bearer.Implementation;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddLoggingSerilog();


builder.Services.AddBearerConfig(builder.Configuration);
builder.Services.AddHATEOASConfig();
builder.Services.AddCorsConfig(builder.Configuration);
builder.Services.AddEmailConfiguration(builder.Configuration);
//builder.Services.AddEvolveConfiguration(builder.Configuration,builder.Environment);
builder.Services.AddRouteConfiguration();
builder.Services.AddMappingConfig();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IEmprestimoFuncionarioService, EmprestimoFuncionarioService>();
builder.Services.AddScoped<IEmprestimoAlunoService, EmprestimoAlunoService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<EmailSender>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHashService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IEmprestimoFuncionarioRepository, EmprestimoFuncionarioRepository>();
builder.Services.AddScoped<IEmprestimoAlunoRepository, EmprestimoAlunoRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped(typeof(XlsxExporterAluno<>));
builder.Services.AddScoped(typeof(CsvExporterAluno<>));
builder.Services.AddScoped(typeof(PdfExporterAluno<>));
builder.Services.AddScoped(typeof(FileExporterFactory<>));

//builder.Services.AddScoped<HypermediaFilterOptions>();

// Add services to the container.

builder.Services.AddControllers(options=>
{
    options.Filters.Add<HypermediaFilter>();
})
    .AddJsonOptions(options=>
    {
        options.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles;
    })
    ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddContextDatabase(
    builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsConfiguration(builder.Configuration);
app.MapControllers();

app.UseHATEOASRoutes();
app.UseScalarConfiguration();

app.Run();
