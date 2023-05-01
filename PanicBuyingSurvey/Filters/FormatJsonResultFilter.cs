using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PanicBuyingSurvey.Filters
{
    public class FormatJsonResultFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, 
                                         ActionExecutionDelegate next)
        {

            var resultContext = await next(); 
            FormatJsonResponse(resultContext);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            FormatJsonResponse(context);
        }

        private void FormatJsonResponse(ActionExecutedContext context)
        {
            if (context.Result is JsonResult result)
            {
                context.Result = new JsonResult
                (
                    new {

                        Data = result.Value,
                        IsSuccess = true
                    }
                );
            }
        }
    }
}