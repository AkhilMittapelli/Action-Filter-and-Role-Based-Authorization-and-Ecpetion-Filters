using Microsoft.AspNetCore.Mvc.Filters;

namespace Technical.Filters
{
    public class MyLogging : Attribute, IActionFilter
    {
        private readonly string _name;

        public MyLogging(string name)
        {
            _name = name;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"After {_name}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Before{_name}");
        }
    }
}
