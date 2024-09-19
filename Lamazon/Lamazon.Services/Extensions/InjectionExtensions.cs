using FluentValidation;
using FluentValidation.AspNetCore;
using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModelValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace Lamazon.Services.Extensions
{
    public static class InjectionExtensions
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IInvoicesRepository, InvoicesRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IInvoicesService, InvoicesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddHttpClient<IGeoTrackerService, GeoTrackerService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
        }

        public static void InjectAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
        }

        public static void InjectFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(s =>
            {
                s.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssemblyContaining<ProductCategoryViewModelValidator>();
        }

        public static IHostBuilder InjectSerilogConfiguration(this IHostBuilder builder, string connectionString)
        {
            var columnOptions = new ColumnOptions
            {
                Store = new Collection<StandardColumn>
                {
                    StandardColumn.Id,
                    StandardColumn.Level,
                    StandardColumn.Message,
                    StandardColumn.TimeStamp,
                },
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn(columnName: "EnvironmentUserName", dataType: SqlDbType.VarChar, dataLength: 50, allowNull: true)
                }
            };

            Log.Logger = new LoggerConfiguration()
                     .Enrich.FromLogContext()
                     .Enrich.WithMachineName()
                     .Enrich.WithEnvironmentUserName()
                     .MinimumLevel.Information()
                     .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                     .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                     .WriteTo.File(path: "./Logs/lamazon-logs-.txt", outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] : {Message}{NewLine:1}{Exception:1}", rollingInterval: RollingInterval.Month)
                     .WriteTo.MSSqlServer(
                         connectionString: connectionString,
                         sinkOptions: new MSSqlServerSinkOptions
                         {
                             TableName = "Logs",
                             AutoCreateSqlTable = true,
                             // Automatically create the table if it doesn't exist
                             // NOTE: Better way is to create migration for the Logs table and set this to 'false'
                         },
                         restrictedToMinimumLevel: LogEventLevel.Information,
                         columnOptions: columnOptions
                     )
                     .CreateLogger();

            builder.UseSerilog();

            return builder;
        }
    }
}
