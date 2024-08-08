using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddHttpClient();

            services.AddHttpContextAccessor();

            services.AddSingleton<IApiClient, ApiClient>();

            return services;
        }
    }
}
