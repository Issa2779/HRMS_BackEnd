namespace HRMS_BackEnd.Middleware
{
    public class ExceptionHandlerMiddleware
    {

        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next) { 
        
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {

                logger.LogError($"Test Exception MW: {ex.Message}");


                httpContext.Response.StatusCode = 500;

                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = Guid.NewGuid(),
                    error = ex.Message,
                };

                await httpContext.Response.WriteAsJsonAsync(error);


            }


        }
    }
}
