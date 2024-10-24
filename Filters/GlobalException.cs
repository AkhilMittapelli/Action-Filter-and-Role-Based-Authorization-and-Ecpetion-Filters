using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Technical.Filters
{
    public class GlobalException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new OkObjectResult(new { err = true, Descrption = "Global", Statuscode = 500, str="Check code" });
           
        }
    }
}
