namespace StudentManagement.API.Configuration
{
	public class AppConfiguration : IAppConfiguration
	{
		public AppConfiguration(IConfiguration config)
		{
			SupportedVersions = config.GetSection("SupportedVersions").Get<APIVersion.Version[]>();
		}

		public APIVersion.Version[] SupportedVersions { get; }

		public APIVersion.Version DefaultVersion => SupportedVersions.Last();
	}
}
