using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Queries;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddLogging(
                loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                    loggingBuilder.AddNLog();
                });

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IUnitOfWok, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IContactByService, ContactByService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFollowupService, FollowupService>();
            services.AddScoped<IFollowupQueryService, FollowupQueryService>();
            services.AddScoped<IBookingQueryService, BookingQueryService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFeedbackQueryService, FeedbackQueryService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISubAreaService, SubAreaService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IMonthListService, MonthListService>();
            services.AddScoped<IComplainRegisterService, ComplainRegisterService>();
            services.AddScoped<IComplainFeedbackService, ComplainFeedbackService>();
            services.AddScoped<ICustomerFollowupService, CustomerFollowupService>();
            services.AddScoped<ITokenClaimService, TokenClaimService>();
            services.AddScoped<ICustomerQueryService, CustomerQueryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAddNewFollowupService, AddNewFollowupService>();
            services.AddScoped<IAddComplainFeebackService, AddComplainFeebackService>();
            services.AddScoped<IMISReportQueryService, MISReportQueryService>();
            services.AddScoped<IContractDetailsService, ContractDetailsService>();
            services.AddScoped<IBuildingDetailsService, BuildingDetailsService>();
            services.AddScoped<IDraftCustomerService, DraftCustomerService>();
            services.AddScoped<IDashboardQueryService, DashboardQueryService>();

            return services;
        }
    }
}