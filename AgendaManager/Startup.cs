using AgendaManager.Core.IoC;
using AgendaManager.Core.Model;
using AgendaManager.Model.Context;
using AgendaManager.Services.IoC;
using AgendaManager.Services.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaManager
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

            services.AddCors();
            
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.Configure<EmailConfig>(Configuration.GetSection("EmailConfig"));

            services.TokenAuth(Configuration);
            
            services.AddControllers();


            services.AddCoreRegistry();
            services.AddServicesRegistry();


            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgendaManager", Version = "v1" });

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new String[] { } }
                });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connectionString = Configuration.GetConnectionString("SQLITE-LOCAL");
            services.AddDbContext<AgendaContext>(op => op.UseSqlite(connectionString));

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => {
                    var server = new OpenApiServer() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" };
                    swagger.Servers = new List<OpenApiServer>() { server };
                });
            });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgendaManager v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
