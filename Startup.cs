using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using FreeCMS.DataAccess;
using FreeCMS.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using FreeCMS.Presentation.Formatters;
using FreeCMS.BussinessLogic;
using FreeCMS.Shared.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FreeCMS
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
            services.AddSingleton<ContentService>();
            services.AddSingleton<ContentRepository>();

            services.AddSingleton<UserManager>();
            services.AddSingleton<UserRepository>();

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.Audience = Configuration["JwtConfiguration:Audience"];
                x.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtConfiguration:TokenKey"])),
                };
            });

            services.AddSingleton<JwtAuthenticateManager>();

            services.AddControllers(opt =>
            {
                opt.InputFormatters.Insert(0, new RawJsonBodyInputFormatter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCMS", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    In=ParameterLocation.Header,
                    Description="Insert a token before authorize.",
                    Name="Authorization",
                    Type=SecuritySchemeType.Http,
                    BearerFormat="JWT",
                    Scheme="bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCMS v1"));
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
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
