using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using web_api_write_and_share.Contracts;
using web_api_write_and_share.Data;
using web_api_write_and_share.Services;

namespace web_api_write_and_share.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
          
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}

