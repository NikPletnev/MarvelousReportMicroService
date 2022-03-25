﻿using MarvelousReportMicroService.API.Consumers;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.DAL.Repositories;
using MassTransit;
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
            services.AddScoped<IServiceService, ServiceService>();
        }

        public static void RegisterProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
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

        public static void AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TransactionsConsumer>();
                x.AddConsumer<LeadConsumer>();
                x.AddConsumer<AccountConsumer>();
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


                });
            });
        }
    }
}