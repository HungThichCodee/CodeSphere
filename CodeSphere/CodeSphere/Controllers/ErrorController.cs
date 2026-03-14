using Microsoft.AspNetCore.Mvc;

namespace CodeSphere.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return this.View("NotFound");
        }
    }
}
