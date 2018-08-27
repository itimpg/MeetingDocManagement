using System;
using Microsoft.AspNetCore.Http;

namespace MeetingDoc.Api.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, Exception exception)
        {
            var errorMessage = exception.Message;
            response.Headers.Add("Application-Error", errorMessage);
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}