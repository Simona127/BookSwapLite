using Microsoft.AspNetCore.Mvc;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("404")]
    public IActionResult Error404() => View();

    [Route("500")]
    public IActionResult Error500() => View();

    [Route("{statusCode}")]
    public IActionResult HandleErrorCode(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("Error404");
        }

        return View("Error500");
    }
}