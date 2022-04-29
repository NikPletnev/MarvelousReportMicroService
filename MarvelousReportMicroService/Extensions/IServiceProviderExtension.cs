using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.API.Consumers;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Helpers;
using NLog.Extensions.Logging;
using System.Data.SqlClient;
using SqlKata.Compilers;
using SqlKata.Execution;
using MassTransit;

namespace MarvelousReportMicroService.API.Extensions
{
    public static class IServiceProviderExtension
    {
        public static void RegisterProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IRequestHelper, RequestHelper>();
            services.AddScoped<IInvoicePaymentService, InvoicePaymentService>();
        }

        public static void RegisterProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IInvoicePaymentRepository, InvoicePaymentRepository>();
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

        public static void AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TransactionsConsumer>();
                x.AddConsumer<LeadConsumer>();
                x.AddConsumer<AccountConsumer>();
                x.AddConsumer<InvoicePaymentConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://80.78.240.16", hst =>
                    {
                        hst.Username("nafanya");
                        hst.Password("qwe!23");
                    });

                    cfg.ReceiveEndpoint("transactionQueue", e =>
                    {
                        e.ConfigureConsumer<TransactionsConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("leadQueue", e =>
                    {
                        e.ConfigureConsumer<LeadConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("accountQueue", e =>
                    {
                        e.ConfigureConsumer<AccountConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("invoicePayments", e =>
                    {
                        e.ConfigureConsumer<InvoicePaymentConsumer>(context);
                    });
                   

                });
            });
        }


    }
}