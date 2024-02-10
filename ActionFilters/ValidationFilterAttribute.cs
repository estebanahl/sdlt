using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace sdlt.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    public ValidationFilterAttribute()
    { }
    public void OnActionExecuting(ActionExecutingContext context) {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments.SingleOrDefault(
            x => x.Value!.ToString()!.Contains("Dto") ||
            x.Value!.ToString()!.Contains("Parameter") ||
            x.Value!.ToString()!.Contains("JsonPatchDocument")
        ).Value;

        if (param is null){
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
            return;
        }
        if(!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
     }
    public void OnActionExecuted(ActionExecutedContext context) { }
}