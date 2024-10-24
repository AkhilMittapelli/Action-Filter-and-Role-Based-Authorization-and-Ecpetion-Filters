using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Technical.Filters
{
    public class CustomDeleteException : ExceptionFilterAttribute
    { 
        public override void OnException(ExceptionContext context)
        {

            context.Result = new OkObjectResult(new { err=true, Description= "Please Check and Enter Valid RollNo", StatusCodes=500, });
     
        }
    }
}
