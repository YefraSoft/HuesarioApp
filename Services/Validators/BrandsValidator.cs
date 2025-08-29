using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;

namespace HuesarioApp.Services.Validators;

public class BrandsValidator(IValidator validator) : IEntityValidator<Brands>
{
    public bool IsValid(Brands entity)
    {
        return validator.IsValidString(entity.Name);
    }
}