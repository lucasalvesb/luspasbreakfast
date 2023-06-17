using Microsoft.AspNetCore.Mvc;

namespace LuspasBreakfast.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() => Problem();
}