using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Repository;
using Repository.Interfaces;
using Repository.MsSqlRepository;
using UniversitySchedule.Authentication;

namespace UniversitySchedule
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddTransient(_ => new ConnectionOptions(Configuration.GetConnectionString("UniversityScheduleConnection")));
			services.AddTransient<IFacultyRepository, MsSqlFacultyRepository>();
			services.AddTransient<IGroupRepository, MsSqlGroupRepository>();
			services.AddTransient<IUserRepository, MsSqlUserRepository>();
			services.AddTransient<IWebPageRepository, MsSqlWebPageRepository>();
			services.AddTransient<IScheduleRepository, MsSqlScheduleRepository>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.RequireHttpsMetadata = false;
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidIssuer = AuthOptions.Issuer,

							ValidateAudience = true,
							ValidAudience = AuthOptions.Audience,

							RequireExpirationTime = true,
							ValidateLifetime = true,

							IssuerSigningKey = AuthOptions.SymmetricSecurityKey,
							ValidateIssuerSigningKey = true,
						};
					});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Home/Index");

			app.UseAuthentication();

			var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".vue"] = "text/html";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });            
        }
    }
}
