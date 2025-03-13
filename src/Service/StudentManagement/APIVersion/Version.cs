namespace StudentManagement.API.Configuration.APIVersion
{
#nullable disable
	public class Version
	{
		public string Major { get; set; }
		public string Minor { get; set; }
		public string Patch { get; set; }
		public string Build { get; set; }

	public override string ToString()
	{
			return $"{Major}.{Minor}.{Patch}";
		}
	}
}
