using LuspasBreakfast.Contracts.Breakfast;
using LuspasBreakfast.Models;
using LuspasBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;

namespace LuspasBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
  private readonly IBreakfastService _breakfastService;

  public BreakfastsController(IBreakfastService breakfastService)
  {
    _breakfastService = breakfastService;
  }

  [HttpPost]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    var breakfast = new Breakfast(
      Guid.NewGuid(),
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      DateTime.UtcNow,
      request.Savory,
      request.Sweet
    );

    _breakfastService.CreateBreakfast(breakfast);


    var response = new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );

    return CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Id },
      value: response);
  }

  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    Breakfast breakfast = _breakfastService.GetBreakfast(id);

    var response = new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );

    return Ok(response);
  }

  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
  {
    var breakfast = new Breakfast(
      id,
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      DateTime.UtcNow,
      request.Savory,
      request.Sweet
    );

    _breakfastService.UpsertBreakfast(breakfast);

    // TODO: return 201 if a breakfast was created
    return NoContent();
  }

    [HttpDelete("{id:guid}")]
  public IActionResult DeleteBreakfast(Guid id)
  {
    _breakfastService.DeleteBreakfast(id);
    return NoContent();
  }
}
