using MarvelousReportMicroService.API.Configuration;
using MarvelousReportMicroService.API.Infrastructure;
using MarvelousReportMicroService.BLL.Configuration;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(BusinessMapper).Assembly, typeof(APIMapper).Assembly);

string _connectionStringVariableName = "CONNECTION_STRING";
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

builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExeptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
