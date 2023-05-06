using BlogJwtNet6.Data;
using BlogJwtNet6.Models;
using BlogJwtNet6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Win32;
using System.Net;
using System.Text;

namespace BlogJwtNet6.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;


        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context, ILoggerFactory logFactory,
            IJwtService jwtService, IUserService userService, IRedisService redisService)
        {
            var logger = logFactory.CreateLogger<JwtMiddleware>();
            logger.LogInformation("d5l el jwt middleware");
            var swaggerPath = context.Request.Path.StartsWithSegments("/swagger");
            var registerPath = context.Request.Path.StartsWithSegments("/api/Users/register");
            if (swaggerPath || registerPath)
            {
                await _next(context);
            }
            else
            {
                logger.LogInformation("not swagger path");
                var authToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (authToken == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
                // Check if logged out 
                var cachedRevokedTokens = redisService.GetRedisClient().GetAllItemsFromList("expiredTokens");
                if (cachedRevokedTokens != null)
                {
                    if (cachedRevokedTokens.Contains(authToken))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }
                }

                var userId =  jwtService.ValidateToken(authToken);
                if (userId != null)
                {
                    context.Items["User"] =  userService.GetById(userId);
                    context.Items["AuthToken"] = authToken;
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }
        }
    }
}
