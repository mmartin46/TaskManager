using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Threading.RateLimiting;

namespace TaskManagerGUI.Middleware
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddLoginRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                ConfigureRateLimiter("login", ref options);
                ConfigureRateLimiter("register", ref options);
            });

            return services;
        }

        private static void ConfigureRateLimiter(string policyName, ref RateLimiterOptions options, string windowLimiter = "fixed")
        {
            options.OnRejected = async (context, _) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = new StringValues(
                            ((int)retryAfter.TotalSeconds).ToString()
                    );
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";
                var result = new { message = "Too many requests. Please try again later." };
                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
            };

            switch (windowLimiter)
            {
                case "fixed":
                {
                    options.AddFixedWindowLimiter(policyName: policyName, fixedOptions =>
                    {
                        fixedOptions.AutoReplenishment = true;
                        fixedOptions.PermitLimit = 10;
                        fixedOptions.Window = TimeSpan.FromMinutes(1);
                    });
                    break;
                }
                case "sliding":
                {
                    options.AddSlidingWindowLimiter(policyName: policyName, fixedOptions =>
                    {
                        fixedOptions.AutoReplenishment = true;
                        fixedOptions.PermitLimit = 10;
                        fixedOptions.Window = TimeSpan.FromMinutes(1);
                    });
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        public static IApplicationBuilder UseLoginRateLimiter(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }
    }
}
