using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tournament.Middleware
{
    public class Middleware1
    {
        private readonly RequestDelegate _next;

        public Middleware1(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("first start");
            await _next(context);
            Console.WriteLine("first end");
        }
    }
}
