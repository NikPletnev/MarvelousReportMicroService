using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Infrastructure;
using MarvelousReportMicroService.BLL.Configuration;
using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.API.Extensions;
using System.Text.Json.Serialization;
using Marvelous.Contracts.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(BusinessMapper).Assembly, typeof(APIMapper).Assembly);

const string _connectionStringVariableName = "REPORT_CONNECTION_STRING";
const string _logDirectoryVariableName = "LOG_DIRECTORY";
const string auth = "https://piter-education.ru:6042";

string connString = builder.Configuration.GetValue<string>(_connectionStringVariableName);
string logDirectory = builder.Configuration.GetValue<string>(_logDirectoryVariableName);

builder.Services.Configure<DbConfiguration>(opt =>
{
    opt.ConnectionString = connString;
});

var config = new ConfigurationBuilder()
           .SetBasePath(logDirectory)
           .AddXmlFile("NLog.config", optional: true, reloadOnChange: true)
           .Build();

builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterProjectServices();
builder.Services.RegisterProjectRepositories();
builder.Services.RegisterSqlKata(connString);
builder.Services.RegisterLogger(config);
builder.Services.AddMassTransit();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExeptionHandler>();

app.Configuration[Microservice.MarvelousAuth.ToString()] = auth;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
