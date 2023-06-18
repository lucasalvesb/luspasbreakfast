using ErrorOr;
using LuspasBreakfast.Models;
namespace LuspasBreakfast.Services.Breakfasts;

    public interface IBreakfastService
    {
  ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
  ErrorOr<Deleted> DeleteBreakfast(Guid id);
  ErrorOr<Breakfast> GetBreakfast(Guid id);
  ErrorOr<UpsertedBreakFast> UpsertBreakfast(Breakfast breakfast);
}
