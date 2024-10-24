using Microsoft.AspNetCore.Mvc.Filters;

namespace Technical.Filters
{
    public class AsyncLogging : Attribute, IAsyncActionFilter
    {
        private readonly string _name;
        public AsyncLogging(string name)
        {

            _name = name;

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            Console.WriteLine($" async After {_name}");
            await next();
            Console.WriteLine($" async Before {_name}");
        }
    }
}
