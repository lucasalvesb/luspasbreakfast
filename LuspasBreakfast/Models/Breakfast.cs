using ErrorOr;
using LuspasBreakfast.ServiceErrors;

namespace LuspasBreakfast.Models;

public class Breakfast {

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;
    public Guid Id { get; }
    public string Name { get; } 
    public string Description { get; } 
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; } 
    public List<string> Sweet { get; }

  private Breakfast(
    Guid id, 
    string name, 
    string description, 
    DateTime startDateTime, 
    DateTime endDateTime, 
    DateTime lastModifiedDateTime, 
    List<string> savory, 
    List<string> sweet)
  {

    Id = id;
    Name = name;
    Description = description;
    StartDateTime = startDateTime;
    EndDateTime = endDateTime;
    LastModifiedDateTime = lastModifiedDateTime;
    Savory = savory;
    Sweet = sweet;
  }

  public static ErrorOr<Breakfast> Create(
    string name, 
    string description, 
    DateTime startDateTime, 
    DateTime endDateTime, 
    List<string> savory, 
    List<string> sweet)
  {

    var id = Guid.NewGuid();
    var lastModifiedDateTime = DateTime.UtcNow;

    if (name.Length is < MinNameLength or > MaxNameLength)
    {
      return Errors.Breakfast.InvalidName;
    }

    if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
    {
      return Errors.Breakfast.InvalidDescription;
    }
    
    var breakfast = new Breakfast(
      id,
      name,
      description,
      startDateTime,
      endDateTime,
      lastModifiedDateTime,
      savory,
      sweet
    );

    return breakfast;
  }
}