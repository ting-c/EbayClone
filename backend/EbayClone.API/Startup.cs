using AutoMapper;
using EbayClone.Api.Extensions;
using EbayClone.API.Settings;
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

namespace EbayClone.API
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
            // get jwt settings from appsettings.json as JwtSettings type
			var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            // add secret for jwt from user secret
            jwtSettings.Secret = Configuration["JwtSecret"];
            // bind JwtSettings type to IConfigurationSection
            Configuration.Bind("Jwt", jwtSettings);
            // make the jwt settings available in app
			services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));

            services.AddAuth(jwtSettings);

            services.AddControllers();
            
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


            // add dependency injection so it injects UnitOfWork when IUnitOfWork is use
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Ebay Clone",
                    Version = "v1"
                });
            });

            services.AddAutoMapper(typeof(Startup));
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

            app.UseAuthorization();

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}