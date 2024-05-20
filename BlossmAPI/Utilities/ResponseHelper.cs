using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Utilities
{
    public class ResponseHelper
    {
        public static IActionResult IfBad(bool rs, string error = null)
        {
            if (rs)
                return new OkResult();
            return new BadRequestObjectResult(error);
        }

        public static IActionResult IfNotFound(object rs)
        {
            if (rs != null)
                return new OkObjectResult(rs);
            return new NotFoundResult();
        }
    }
}
