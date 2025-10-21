using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace CarPark.Application.Common.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
        {
            try
            {
                await next(ctx);
            }
            catch (ValidationException ex)
            {
                await WriteJsonAsync(ctx, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await WriteJsonAsync(ctx, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ConflictException ex)
            {
                await WriteJsonAsync(ctx, StatusCodes.Status409Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                await WriteJsonAsync(ctx, StatusCodes.Status500InternalServerError, "Unexpected error.", ex.Message);
            }
        }

        private static async Task WriteJsonAsync(HttpContext ctx, int status, string error, string? detail = null)
        {
            ctx.Response.StatusCode = status;
            ctx.Response.ContentType = "application/json";

            var payload = new { error, detail };
            var json = JsonSerializer.Serialize(payload);
            await ctx.Response.WriteAsync(json);
        }
    }
}