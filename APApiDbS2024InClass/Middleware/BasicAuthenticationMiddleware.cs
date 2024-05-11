//using System;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;

//namespace APApiDbS2024InClass.Middleware;

//public class BasicAuthenticationMiddleware
//{
//    private const string USERNAME = "john.doe";
//    private const string PASSWORD = "VerySecret!";
//    private readonly RequestDelegate _next;

//    public BasicAuthenticationMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        // Bypass authentication for [AllowAnonymous]
//        var endpoint = context.GetEndpoint();
//        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
//        {
//            await _next(context);
//            return;
//        }

//        // Retrieve the Authorization header
//        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader) || string.IsNullOrWhiteSpace(authHeader))
//        {
//            context.Response.StatusCode = 401;
//            await context.Response.WriteAsync("Authorization header not provided.");
//            return;
//        }

//        try
//        {
//            // Assuming the scheme and parameters are separated by a space
//            var authValues = authHeader.ToString().Split(' ');
//            if (authValues.Length != 2 || authValues[0] != "Basic")
//            {
//                throw new ArgumentException("Invalid Authorization header format.");
//            }

//            // Decode from Base64 to string
//            var encodedCredentials = authValues[1];
//            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
//            var credentials = decodedCredentials.Split(':');

//            if (credentials.Length != 2)
//            {
//                throw new ArgumentException("Invalid credentials format.");
//            }

//            // Check username and password
//            var username = credentials[0];
//            var password = credentials[1];
//            if (username == USERNAME && password == PASSWORD)
//            {
//                await _next(context);
//            }
//            else
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Incorrect credentials provided.");
//            }
//        }
//        catch (Exception ex)
//        {
//            // Handle parsing errors or other exceptions
//            context.Response.StatusCode = 400; // Bad Request
//            await context.Response.WriteAsync($"Error processing request: {ex.Message}");
//        }
//    }
//}

//public static class BasicAuthenticationMiddlewareExtensions
//{
//    public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<BasicAuthenticationMiddleware>();
//    }
//}
