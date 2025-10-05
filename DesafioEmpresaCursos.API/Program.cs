using DesafioEmpresaCursos.Domain.Interfaces.Repositories;
using DesafioEmpresaCursos.Domain.Interfaces.Services;
using DesafioEmpresaCursos.Domain.Services;
using DesafioEmpresaCursos.Infra.Data.Contexts;
using DesafioEmpresaCursos.Infra.Repositories;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

builder.Services.AddScoped<IAlunoService, AlunoService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();