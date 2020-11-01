using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AgaSoft.Client.Interfaces;
using AgaSoft.Client.Model;
using AgaSoft.Client.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AgaSoft.Client.Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                  });
            });
            services.AddControllers();
            services.AddDbContext<AgaSoftRepositoryContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            var optionsBuilder = new DbContextOptionsBuilder<AgaSoftRepositoryContext>();
            optionsBuilder.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSingleton<IAuthenticationProvider>(s => new AuthenticationProvider(optionsBuilder.Options));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SmartOrder API",
                    Description = "Description of the API that are called for the SamrtOrder project",
                    Contact = new OpenApiContact
                    {
                        Name = "Ermal Halilaga",
                        Email = string.Empty,
                        Url = new Uri("https://www.linkedin.com/in/ermal-halilaga-21ba8ab9"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //services.AddSingleton(typeof(IAuthenticationProvider), typeof(AuthenticationProvider));
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateIssuerSigningKey = true,

            //        //Importante: indicare lo stesso Issuer, Audience e chiave segreta
            //        //usati anche nel JwtTokenMiddleware
            //        ValidIssuer = "Issuer",
            //        ValidAudience = "Audience",
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //          Encoding.UTF8.GetBytes("MiaChiaveSegreta")
            //      ),
            //        //Tolleranza sulla data di scadenza del token
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartOrder API V1");
                c.RoutePrefix = string.Empty;

            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
