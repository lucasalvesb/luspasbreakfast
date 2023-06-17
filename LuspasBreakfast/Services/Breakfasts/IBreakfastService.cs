using LuspasBreakfast.Models;
namespace LuspasBreakfast.Services.Breakfasts;

    public interface IBreakfastService
    {
        void CreateBreakfast(Breakfast breakfast);
  void DeleteBreakfast(Guid id);
  Breakfast GetBreakfast(Guid id);
  void UpsertBreakfast(Breakfast breakfast);
}
