using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;

namespace HuesarioApp.Services.Validators;

public class RegisterValidator : IEntityValidator<RegisterBody>
{
    private readonly PrimitivesValidator _validator = new();

    public bool IsValid(RegisterBody entity)
    {
        return (
            _validator.IsValidUser(entity.username) &&
            _validator.IsValidPassword(entity.password) &&
            _validator.IsValidUser(entity.name) &&
            _validator.IsValidNumber(entity.roleId)
        );
    }
}