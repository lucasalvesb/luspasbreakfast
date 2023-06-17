using LuspasBreakfast.Models;

namespace LuspasBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
  private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();
  public void CreateBreakfast(Breakfast breakfast)
  {
    //repository or entity framework to store the database
    _breakfasts.Add(breakfast.Id, breakfast);
  }

  public Breakfast GetBreakfast(Guid id)
  {
    return _breakfasts[id];
  }
}