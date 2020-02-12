using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndSharedPrototip.Contracts;
using WriteAndSharedPrototip.Data;
using WriteAndSharedPrototip.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WriteAndSharedPrototip.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
