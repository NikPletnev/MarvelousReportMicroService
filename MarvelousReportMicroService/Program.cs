using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Extensions;
using MarvelousReportMicroService.API.Infrastructure;
using MarvelousReportMicroService.BLL.Configuration;
using MarvelousReportMicroService.DAL.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(BusinessMapper).Assembly, typeof(APIMapper).Assembly);

string _connectionStringVariableName = "REPORT_CONNECTION_STRING";
string connString = builder.Configuration.GetValue<string>(_connectionStringVariableName);

builder.Services.Configure<DbConfiguration>(opt =>
{
    opt.ConnectionString = connString;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterProjectServices();
builder.Services.RegisterProjectRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExeptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
