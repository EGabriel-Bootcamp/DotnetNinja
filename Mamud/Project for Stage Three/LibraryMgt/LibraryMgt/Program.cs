using LibraryMgt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
//.AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.IgnoreNullValues = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDbContext<ApiContext>(o => o.UseInMemoryDatabase("LibraryDb"));
builder.Services.AddDbContext<ApiContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddSwaggerGen();

//optionsBuilder.UseNpgsql(@"Host=localhost;Database=entitycore;Username=postgres;Password=mypassword");

//services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(
//                    Configuration.GetConnectionString("DefaultConnection")));

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

