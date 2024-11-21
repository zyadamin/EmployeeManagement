using Audit.DAL.Context;
using EmployeeManagement.BLL.Interfaces;
using EmployeeManagement.BLL.Services;
using EmployeeManagement.DAL.Context;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.DAL.UnitOfWorks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using IUnitOfWork = EmployeeManagement.BLL.Interfaces.IUnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In Startup.cs or Program.cs
builder.Services.AddScoped<DbConnection>(provider =>
{
    var connectionString = provider.GetRequiredService<IConfiguration>()
                                    .GetConnectionString("DbConnection");
    var connection = new SqlConnection(connectionString);
    connection.Open(); // Ensure connection is open for sharing
    return connection;
});

// Register MainDbContext and AuditDbContext
builder.Services.AddDbContext<MainDbContext>((provider, options) =>
{
    var connection = provider.GetRequiredService<DbConnection>();
    options.UseSqlServer(connection);
});

builder.Services.AddDbContext<AuditDbContext>((provider, options) =>
{
    var connection = provider.GetRequiredService<DbConnection>();
    options.UseSqlServer(connection);
});
//builder.Services.AddDbContext<MainDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//builder.Services.AddDbContext<AuditDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<Audit.BLL.Interfaces.IAuditService, Audit.BLL.Services.AuditService>();
builder.Services.AddScoped<Audit.BLL.Interfaces.IUnitOfWork, Audit.DAL.UnitOfWorks.UnitOfWork>();
builder.Services.AddScoped<Audit.BLL.Interfaces.IAuditRepository, Audit.DAL.Repositories.AuditRepository>();

builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseAuthorization();

app.MapControllers();

app.Run();
