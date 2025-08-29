using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;

namespace HuesarioApp.Services.Validators;

public class ModelsValidator(IValidator validator) : IEntityValidator<VehicleModels>
{
    public bool IsValid(VehicleModels entity)
    {
        return
            validator.IsValidString(entity.Name) &&
            validator.IsValidString(entity.Engine) &&
            validator.IsValidYear(entity.Year) &&
            validator.IsValidNumber(entity.BrandId) &&
            Enum.IsDefined(typeof(TransmissionType), entity.Transmission);
    }
}