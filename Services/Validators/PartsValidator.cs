using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;

namespace HuesarioApp.Services.Validators;

public class PartsValidator(IValidator validator) : IEntityValidator<Parts>
{
    public bool IsValid(Parts entity)
    {
        return
            validator.IsValidString(entity.Name) &&
            Enum.IsDefined(typeof(Side), entity.Side) &&
            Enum.IsDefined(typeof(PartCategory), entity.PartCategory) &&
            validator.IsValidNumber(entity.Stock) &&
            validator.IsValidNumber((int)entity.Price) &&
            validator.IsValidNumber(entity.ModelId);
    }
}