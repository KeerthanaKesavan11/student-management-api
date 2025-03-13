namespace StudentManagement.API.Configuration
{
	public interface IAppConfiguration
	{
		public APIVersion.Version DefaultVersion { get; }
		public APIVersion.Version[] SupportedVersions { get; }
	}
}
