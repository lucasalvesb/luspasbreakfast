using BuberBreakfast.Contracts.Breakfast;
using Microsoft.AspNetCore.Mvc;

namespace LuspasBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
  [HttpPost("/breakfasts")]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    return Ok(request);
  }

  [HttpGet("/breakfasts/{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    return Ok(id);
  }
}
