using Campaigns.API.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Azakaw.Complaints.API.Swagger
{
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}