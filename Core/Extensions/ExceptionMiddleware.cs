using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";

            string message = SetErrorMessageAndCode(httpContext, e);

            return httpContext.Response.WriteAsync(new ErrorDetail
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }

        private  string SetErrorMessageAndCode(HttpContext httpContext, Exception e)
        {
            string message;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            else if (e.GetType() == typeof(AuthenticationException))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                message = e.Message;
            } else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                message = e.Message;
            }
            else
            {
                message = "Internal Server Error";
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            return message;
        }
    }
}
