namespace StudentManagement.API.Configuration;

public static class CorsConfigurationExtensions
{
	public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
	{
		var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");
		if (!string.IsNullOrWhiteSpace(allowedOrigins))
		{
			var origins = allowedOrigins.Split(",");
			services.AddCors(
					options => options.AddPolicy(
						"GetAgreements",
						builder =>
						{
							builder.WithOrigins(origins) //NOSONAR
								.SetIsOriginAllowedToAllowWildcardSubdomains()
								.WithMethods("GET")
								.WithHeaders("Authorization", "User-Agent", "Keep-Alive", "Accept", "Content-Type", "Origin");
						}
					));
		}

		return services;
	}
}
