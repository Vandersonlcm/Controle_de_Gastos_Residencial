using ControleGastos.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURAÇÃO DOS SERVIÇOS

// Adiciona suporte aos Controllers.
builder.Services.AddCors(options =>
{
    options.AddPolicy("React",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

// Configura o Entity Framework para utilizar o banco MySQL.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Obtém a string de conexão do arquivo appsettings.json.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Configura o provedor MySQL e detecta automaticamente a versão do servidor.
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    );
});

// Configuração do Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CONFIGURAÇÃO DO PIPELINE HTTP

// Habilita o Swagger somente em ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona requisições HTTP para HTTPS.
app.UseHttpsRedirection();

app.UseCors("React");

// Middleware de autorização.
app.UseAuthorization();

// Mapeia todos os Controllers da aplicação.
app.MapControllers();

// Inicializa a aplicação.
app.Run();