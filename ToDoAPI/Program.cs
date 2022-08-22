using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;

var builder = WebApplication.CreateBuilder(args);
string PolicyName = "AllowWebApps";
string ConnectionStringName = "ToDo";
// Add services to the container.

builder.Services.AddDbContext<ToDoContext>(Options => 
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringName));
});

builder.Services.AddCors(Options => 
{
    Options.AddPolicy(PolicyName, Policy =>
    {
        Policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
                                                
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseStaticFiles();

app.UseCors(PolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
