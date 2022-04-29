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
            services.AddScoped<ITransactionFeeService, TransactionFeeService>();
        }

        public static void RegisterProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IInvoicePaymentRepository, InvoicePaymentRepository>();
            services.AddScoped<ITransactionFeeRepository, TransactionFeeRepository>();

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
                x.AddConsumer<ServiceConsumer>();
                x.AddConsumer<TransactionFeeConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                   

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
                    cfg.ReceiveEndpoint("ServiceQueue", e =>
                    {
                        e.ConfigureConsumer<ServiceConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("TransactionFeeQueue", e =>
                    {
                        e.ConfigureConsumer<TransactionFeeConsumer>(context);
                    });
                });
            });
        }


    }
}