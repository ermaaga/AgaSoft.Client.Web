using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgaSoft.Client.Interfaces;
using AgaSoft.Client.Model;
using AgaSoft.Client.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgaSoft.Client.Web
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
            services.AddControllers();
            services.AddDbContext<AgaSoftRepositoryContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            var optionsBuilder = new DbContextOptionsBuilder<AgaSoftRepositoryContext>();
            optionsBuilder.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));

         
                 services.AddSingleton<IAuthenticationProvider>(s => new AuthenticationProvider(optionsBuilder.Options));                
            
            //services.AddSingleton(typeof(IAuthenticationProvider), typeof(AuthenticationProvider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
