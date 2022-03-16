using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Repositories;
using NLog.Extensions.Logging;

namespace MarvelousReportMicroService.API.Extensions
{
    public static class IServiceProviderExtension
    {
        public static void RegisterProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void RegisterProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void RegisterLogger(this IServiceCollection service, IConfiguration config)
        {
            service.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddNLog(config);
            });
        }
    }
}