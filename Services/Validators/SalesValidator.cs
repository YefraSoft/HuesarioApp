using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;

namespace HuesarioApp.Services.Validators;

public class SalesValidator(IValidator validator) : IEntityValidator<SalesEntity>
{
    public bool IsValid(SalesEntity entity)
    {
        return
            validator.IsValidNumber(entity.PartId) &&
            validator.IsValidString(entity.CustomPartName) &&
            !string.IsNullOrWhiteSpace(entity.ImageUrl) &&
            validator.IsValidNumber(entity.Quantity) &&
            validator.IsValidNumber((int)entity.Price) &&
            Enum.IsDefined(typeof(PaymentMethods), entity.PaymentMethod);
    }
}