using System.ComponentModel.DataAnnotations;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;

namespace backEnd;

public class DateValidationAttribute : ValidationAttribute
{
    public DateValidationAttribute()
    {
    }
    public DateOnly StartDate { get; set;}
    public DateOnly EndDate { get; set;}
    public string GetErrorMessage() =>
        $"An start date ({StartDate}) can't be after the end date ({EndDate}) .";

    protected override ValidationResult? IsValid(
      object? value, ValidationContext validationContext)
    {
        var theEvent = validationContext.ObjectInstance as EventForCreationDto; // el as debería convertir   
        StartDate = theEvent!.StartDate;  
        EndDate = theEvent!.EndDate;  
        var endDate = value as DateOnly?;

        if (StartDate.CompareTo(EndDate) > 0)
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}
