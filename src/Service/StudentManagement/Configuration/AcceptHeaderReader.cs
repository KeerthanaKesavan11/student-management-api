using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Net.Http.Headers;

namespace StudentManagement.API.Configuration
{
    public class AcceptHeaderReader : MediaTypeApiVersionReader
    {
        private readonly IAppConfiguration _appConfig;

        public AcceptHeaderReader(IAppConfiguration appConfig) => _appConfig = appConfig;

        protected override string ReadAcceptHeader(ICollection<MediaTypeHeaderValue> accept)
        {
            if (accept == null || !accept.Any())
            {
                return _appConfig.DefaultVersion.Major;
            }

            var mediaTypes = accept.ToArray();

            foreach (var parameters in mediaTypes.Select(x => x.Parameters))
            {
                foreach (NameValueHeaderValue parameter in parameters)
                {
                    var parameterValue = parameter.Value.ToString();
                    if (parameter.Name == "version" && !string.IsNullOrEmpty(parameterValue))
                    {
                        return parameterValue;
                    }
                }
            }

            return _appConfig.DefaultVersion.Major;
        }
    }
}
