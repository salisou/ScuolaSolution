using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Generazione dello scaffold:
//  Scaffold-DbContext "Data Source=MOUSSA\SQLEXPRESS;Initial Catalog=ScuolaDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context ScuolaDbContext -ContextDir Data -DataAnnotations -Force

// Add services to the container.
var strConn = builder.Configuration.GetConnectionString("ConnezioneLaDbScula")
    ?? throw new InvalidOperationException("Connection string 'ConnezioneLaDbScula' not found.");
//builder.Services.AddDbContext<ScuolaDbContext>(options =>
//    options.UseSqlServer(strConn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
