using Magic_Villa_VillaApi.Data;
using Magic_Villa_VillaApi.Logging;
using Magic_Villa_VillaApi.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Action<MvcOptions> myOptions = delegate (MvcOptions options) { options.ReturnHttpNotAcceptable = true; };

//Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("logs/myVillaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();



builder.Services.AddControllers(/*myOptions*/).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddSingleton(typeof(ILogging), typeof(Logging));
builder.Services.AddSingleton(typeof(ILogging<>), typeof(LoggingGeneric<>));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));



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
