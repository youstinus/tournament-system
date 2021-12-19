using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tournament.Middleware
{
    public class ExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExampleMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do some work before invoking controller's actions

            await _next(context);

            // Do some work after invoking controller's actions
        }
    }
}
