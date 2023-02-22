using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
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

            return services;
        }
    }
}