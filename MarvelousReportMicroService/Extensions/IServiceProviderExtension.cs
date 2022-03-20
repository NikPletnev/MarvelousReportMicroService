using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NLog.Extensions.Logging;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;

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
            service.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);
            service.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddNLog(config);
            });
        }

        public static void RegisterSqlKata(this IServiceCollection service, string connectionString)
        {
            service.AddScoped(
                provider =>
                {
                    var connection = new SqlConnection(connectionString);
                    var compiler = new SqlServerCompiler();
                    var queryFactory = new QueryFactory(connection, compiler);

                    return queryFactory;
                });
        }
    }
}