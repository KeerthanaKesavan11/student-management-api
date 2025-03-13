using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Net;

namespace StudentManagement.API.Configuration
{
    public class ApiVersioningErrorResponseProvider : IErrorResponseProvider
    {
        private readonly string errorTitle = "Bad Request";
        private readonly string errorDetail = "API version is not supported";

        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            var errorResponse = new Error[]
            {
                new Error()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = errorTitle,
                    Source =  new Source
                    {
                        Pointer = context.Request.Path,
                        Parameter = context.Request.QueryString.Value
                    },
                    Detail = errorDetail,
                    HttpStatusCode = HttpStatusCode.BadRequest
                }
            };

            var response = new ObjectResult(errorResponse);
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            return response;
        }
    }

    public class Error
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Source Source { get; set; }
        public string Detail { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class Source
    {
        public string Pointer { get; set; }
        public string Parameter { get; set; }
    }
}

