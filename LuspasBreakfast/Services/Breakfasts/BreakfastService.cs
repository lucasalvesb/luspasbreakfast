using ErrorOr;
using LuspasBreakfast.Models;
using LuspasBreakfast.ServiceErrors;

namespace LuspasBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
  private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
  public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
  {
    //repository or entity framework to store the database
    _breakfasts.Add(breakfast.Id, breakfast);

    return Result.Created;
  }

  public ErrorOr<Deleted> DeleteBreakfast(Guid id)
  {
    _breakfasts.Remove(id);

    return Result.Deleted;
  }

  public ErrorOr<Breakfast> GetBreakfast(Guid id)
  {
    if (_breakfasts.TryGetValue(id, out var breakfast))
    {
      return breakfast;
    }
    
    return Errors.Breakfast.NotFound;
  }

  public ErrorOr<UpsertedBreakFast> UpsertBreakfast(Breakfast breakfast)
  {
    var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
    _breakfasts[breakfast.Id] = breakfast;

    return new UpsertedBreakFast(isNewlyCreated);
  }
}