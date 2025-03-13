using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;

namespace Emis.Agreements.Services.Api.Configuration
{
	public static class JsonSerializerConfigurationExtensions
	{
		public static IMvcBuilder AddJsonSerialzierOptions(this IMvcBuilder builder)
		{
			builder.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			});


			return builder;
		}
	}
}
