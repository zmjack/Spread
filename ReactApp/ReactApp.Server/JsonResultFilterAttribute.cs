using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace Seven;

public class JsonResultFilter : Attribute, IResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult result)
        {
            var httpContext = context.HttpContext;
            var options = httpContext.RequestServices.GetRequiredService<IOptions<JsonOptions>>().Value.JsonSerializerOptions;
            var declaredType = result.DeclaredType!;

            var content = JsonSerializer.Serialize(result.Value, declaredType, options);
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "application/json;charset=utf-8",
                Content = content,
            };
        }
    }
}
