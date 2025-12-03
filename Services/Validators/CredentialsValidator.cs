using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;

namespace HuesarioApp.Services.Validators;

public class CredentialsValidator() : IEntityValidator<LoginBody>
{
    private readonly PrimitivesValidator _validator = new();

    public bool IsValid(LoginBody entity)
    {
        return _validator.IsValidUser(entity.username) &&
               _validator.IsValidPassword(entity.password);
    }
}