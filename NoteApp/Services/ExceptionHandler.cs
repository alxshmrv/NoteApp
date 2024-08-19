using Microsoft.AspNetCore.Diagnostics;
using NoteApp.Exceptions;
using NoteApp.Exeptions;
using System.Net;
using System.Net.Mime;

namespace NoteApp.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            switch (exception)
            {
                case UserNotFoundException userNotFoundException:
                    httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await httpContext.Response.WriteAsync(userNotFoundException.Message);
                    return true;                   
                case NoteNotFoundException noteNotFoundException:
                    httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await httpContext.Response.WriteAsync(noteNotFoundException.Message);
                    return true;                    
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync(string.Empty);
                    return false;                    
            }
        }
    }
}
