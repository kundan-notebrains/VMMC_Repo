using System.Diagnostics;
using System.Net;
using VMMC.Errors;

namespace VMMC.Middleware
{
    public class ExceptionMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> Logger { get; }
        private IHostEnvironment Env { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                ApiError responses;
                HttpStatusCode statusCodes;
                string message;
                var exceptionType = ex.GetType();
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCodes = HttpStatusCode.Forbidden;
                    message = "You are not authorised";
                }
                else
                {
                    statusCodes = HttpStatusCode.InternalServerError;
                    message = "Some unknown error occured";
                }

                if (Env.IsDevelopment())
                {
                    responses = new ApiError((int)statusCodes, ex.Message, ex.StackTrace!.ToString());
                }
                else
                {
                    responses = new ApiError((int)statusCodes, message, ex.StackTrace!.ToString());

                }

                var st = new StackTrace(ex);
                var sf = st.GetFrame(0);

                var currentMethodName = sf!.GetMethod();

                Logger.LogError(ex, string.Concat(currentMethodName!.Name, ex.Message));
                context.Response.StatusCode = (int)statusCodes;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responses.ToString());
            }
        }
    }
}
