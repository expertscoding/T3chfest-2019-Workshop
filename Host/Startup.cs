using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Host.Data;
using Host.Configuration;
using IdentityServer.ActiveDirectory;

namespace Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryClients(Clients.Get())
                .AddAspNetIdentity<ApplicationUser>();
            //.AddActiveDirectoryIdentity<ApplicationUser>(options =>
            //{
            //    options.DomainName = Configuration.GetSection("ActiveDirectory")?["DomainName"];
            //    options.DomainUser = Configuration.GetSection("ActiveDirectory")?["DomainUser"];
            //    options.DomainCryptPassword = Configuration.GetSection("ActiveDirectory")?["DomainPassword"];
            //});

            services.AddAuthentication()
                //Google Developer Console https://console.cloud.google.com/apis/credentials
                .AddGoogle(options =>
                {
                    options.ClientId = "813017584167-dqd2qoo9oau2khmg0binpefnjgq5udar.apps.googleusercontent.com";
                    options.ClientSecret = "Vh0P8HMQbIDJ4E2PK5XEkiQj";
                })
                //Microsoft Developer Console https://apps.dev.microsoft.com/#/appList
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "2d8b1dbd-7bb4-4098-b974-541523a01565";
                    options.ClientSecret = "pbmsoaSCU59625@^mPNHJ^+";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // UseAuthentication not needed -- UseIdentityServer add this
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
