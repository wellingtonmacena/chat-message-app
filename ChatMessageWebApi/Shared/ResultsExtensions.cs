using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace posterr_webapi.src.Shared
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess && result.Value == null)
                return new NotFoundResult();

            if (result.IsSuccess)
                return new OkObjectResult(result.Value);

            if (result.Errors.Any())
                return new NotFoundObjectResult(result.Errors);

            return new BadRequestObjectResult(result.Errors);
        }
    }
}