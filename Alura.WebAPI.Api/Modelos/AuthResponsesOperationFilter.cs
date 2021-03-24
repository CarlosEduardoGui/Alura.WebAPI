using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Alura.WebAPI.Api.Modelos
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Responses.Add(
                "401",
                new Response { Description = "Unauthorized" });
        }
    }
}
