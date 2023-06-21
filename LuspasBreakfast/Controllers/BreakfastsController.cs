using ErrorOr;
using LuspasBreakfast.Contracts.Breakfast;
using LuspasBreakfast.Models;
using LuspasBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;

namespace LuspasBreakfast.Controllers;


public class BreakfastsController : ApiController
{
  private readonly IBreakfastService _breakfastService;

  public BreakfastsController(IBreakfastService breakfastService)
  {
    _breakfastService = breakfastService;
  }

  [HttpPost]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.Create(
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      request.Savory,
      request.Sweet
    );

    if (requestToBreakfastResult.IsError)
    {
      return Problem(requestToBreakfastResult.Errors);
    }

    var breakfast = requestToBreakfastResult.Value;

    ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

    return createBreakfastResult.Match(
      created => CreatedAtGetBreakfast(breakfast),
      errors => Problem(errors)
    );
  }



  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfast(Guid id)
  {
    ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

    return getBreakfastResult.Match(
      breakfast => Ok(MapBreakfastResponse(breakfast)),
      errors => Problem(errors)
    );

  }

  

  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
  {
    ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.Create(
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      request.Savory,
      request.Sweet,
      id
    );

    if (requestToBreakfastResult.IsError)
    {
      return Problem(requestToBreakfastResult.Errors);
    }

    var breakfast = requestToBreakfastResult.Value;

    ErrorOr<UpsertedBreakFast> upsertedBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

    // TODO: return 201 if a breakfast was created
    return upsertedBreakfastResult.Match(
      upserted => upserted.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
      errors => Problem(errors)
    );
  }

    [HttpDelete("{id:guid}")]
  public IActionResult DeleteBreakfast(Guid id)
  {
    ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);
    return deleteBreakfastResult.Match(
      deleted => NoContent(),
      errors => Problem(errors)
    );
  }

  private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
  {
    return new BreakfastResponse(
          breakfast.Id,
          breakfast.Name,
          breakfast.Description,
          breakfast.StartDateTime,
          breakfast.EndDateTime,
          breakfast.LastModifiedDateTime,
          breakfast.Savory,
          breakfast.Sweet
        );
  }

    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
  {
    return CreatedAtAction(
      actionName: nameof(GetBreakfast),
      routeValues: new { id = breakfast.Id },
      value: MapBreakfastResponse(breakfast));
  }

}
