using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserCustom.Areas.Identity.Data;
using UserCustom.Data;

[assembly: HostingStartup(typeof(UserCustom.Areas.Identity.IdentityHostingStartup))]
namespace UserCustom.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserCustomContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserCustomContextConnection")));

                services.AddDefaultIdentity<UserCustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UserCustomContext>();
            });
        }
    }
}