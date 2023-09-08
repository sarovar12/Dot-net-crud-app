using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
namespace Crud.Helpers
{
    public class APIKeyAuth : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "x-api-key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }
            else
            {
                var check = CheckAPIkey(potentialApiKey);
                if (!check)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            await next();
        }
        public static bool CheckAPIkey(string Key)
        {
            if (Key != null)
            {
                return true;
            }

            return false;
        }
    }
}