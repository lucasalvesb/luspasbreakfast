using LuspasBreakfast.Models;
namespace LuspasBreakfast.Services.Breakfasts;

    public interface IBreakfastService
    {
        void CreateBreakfast(Breakfast breakfast);
  Breakfast GetBreakfast(Guid id);
}
