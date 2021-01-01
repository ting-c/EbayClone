using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Builder;
using EbayClone.Services.Settings;

namespace EbayClone.Api.Extensions
{
	public static class AuthExtensions
	{
		public static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
		{
			services
				.AddAuthorization()
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = jwtSettings.Issuer,
						ValidAudience = jwtSettings.Issuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
						ClockSkew = TimeSpan.Zero
					};
				});

			return services;
		}

		public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();

			return app;
		}
	}
}
