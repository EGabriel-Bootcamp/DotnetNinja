using LibraryManagementApp.Interfaces;
using LibraryManagementApp.LibManagementDbContext;
using LibraryManagementApp.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApp.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration _config)
        {
            services.AddScoped<IBook, BookService>();
            services.AddScoped<IPublisher, PublisherService>();
            services.AddScoped<IAuthor, AuthorService>();
            services.AddDbContext<LibraryMgtDbContext>(opt =>
            {
                opt.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
