using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Produto.Application.Interfaces;
using Produto.Application.Services;
using Produto.Domain.Interfaces;
using Produto.Infrastructure.Data;
using Produto.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.IsDevelopment();

// Configuração do DbContext com Oracle
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<ProdutoDbContext>(options =>
    options.UseOracle(connectionString));

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProdutoAPI",
        Version = "v1",
        Description = "API para gerenciamento de produtos",
        Contact = new OpenApiContact
        {
            Name = "Adonay Rocha",
            Email = "rm558782@fiap.com.br"
        }
    });

});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProdutoAPI v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();