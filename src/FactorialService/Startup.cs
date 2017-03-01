using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace MicroservicesDemo.FactorialService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseOwin(pipeline => pipeline.UseNancy(options =>
            {
                options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
            }));
        }
    }
}
