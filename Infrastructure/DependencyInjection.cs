using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(connectionString
                                //   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName) -- use this to tell EFcore where to look for migrations. By default will look where 'DbContext' is defined
                        ));

            //services.AddScoped<IApplicationDbcontext>(provider => provider.GetService<ApplicationDbContext>()); -- direct factory method to add more logic
            services.AddScoped<IApplicationDbcontext, ApplicationDbContext>();
            return services;
        }
    }
}
