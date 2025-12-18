using EducacaoOnline.Core.DomainObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducacaoOnline.Core.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException ex:
                    context.Result = new NotFoundObjectResult(new { message = ex.Message });
                    break;

                case InvalidOperationException ex:
                    context.Result = new BadRequestObjectResult(new { message = ex.Message });
                    break;

                case UnauthorizedAccessException ex:
                    context.Result = new UnauthorizedResult();
                    break;

                default:
                    context.Result = new StatusCodeResult(500);
                    break;
            }

            context.ExceptionHandled = true;
        }
    }

}
