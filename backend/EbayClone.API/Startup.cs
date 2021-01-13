using System.Collections.Generic;
using AutoMapper;
using EbayClone.Api.Extensions;
using EbayClone.Services.Settings;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using EbayClone.Data;
using EbayClone.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CloudinaryDotNet;
using System;

namespace EbayClone.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// cors
			services.AddCors(options =>
            {
                options.AddPolicy(name: "localhost",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

			// httpClient for making http requests
			services.AddHttpClient("imgbb", c =>
			{   
                // imgbb url with api key parameter
				c.BaseAddress = new Uri($"https://api.imgbb.com/1/upload?key={Configuration["imgbbApiKey"]}");
            });

			// add secret for jwt from user secret
			var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            jwtSettings.Secret = Configuration["JwtSecret"];
            // inject updated jwtSetting
            services.AddSingleton(jwtSettings);

            services.AddControllers()
                // prevent potential loop warning
                .AddNewtonsoftJson(options => 
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            
            var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("Dev"));
        
            // add user secret password for DB
            builder.Password = Configuration["EbayCloneSQLPassword"];

            var connectionString = builder.ToString();

            // add DbContext and run migrations in EbayClone.Data
            services.AddDbContext<EbayCloneDbContext>(options =>
                options.UseSqlServer(connectionString,
                x => x.MigrationsAssembly("EbayClone.Data")));

            // add Identity with additional config
			services.AddIdentity<User, Role>(options => 
            {
				options.Password.RequiredLength = 8;
            })
                // add EF implementation
	            .AddEntityFrameworkStores<EbayCloneDbContext>()
				//default token providers - generate tokens for a password reset, 2 factor authentication, change email and telephone
	            .AddDefaultTokenProviders();


            // dependency injection for interfaces
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IFilePathService, FilePathService>();

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Ebay Clone",
                    Version = "v1"
                });
                // config to test Bearer token through SwaggerUI
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT containing userid claim",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
				});

				var security =
					new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Id = "Bearer",
									Type = ReferenceType.SecurityScheme
								},
								UnresolvedReference = true
							},
							new List<string>()
						}
					};
				options.AddSecurityRequirement(security);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddAuth(jwtSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Middleware to serve SwaggerUI, specify the Swagger JSON Endpoint
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ebay Clone V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("localhost");

            app.UseAuth();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}