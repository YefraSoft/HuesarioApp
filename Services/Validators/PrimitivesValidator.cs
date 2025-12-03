using System.Text.RegularExpressions;
using HuesarioApp.Interfaces.DataServices;

namespace HuesarioApp.Services.Validators;

public partial class PrimitivesValidator : IValidator
{
    private readonly Regex _stringRegex = StringRegex();

    [GeneratedRegex(@"^([a-zA-Z]+\s\d+|\d+(\.\d{1,2})?\s[a-zA-Z\s]{1,45}|[a-zA-Z\s]{1,50}|\d+|\d+\.\d{1,2})$")]
    private static partial Regex StringRegex();

    [GeneratedRegex(@"^[a-zA-Z0-9._]{3,20}$")]
    private static partial Regex UserRegex();

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
    private static partial Regex PasswordRegex();

    public bool IsValidString(string data)
    {
        return !string.IsNullOrWhiteSpace(data) &&
               _stringRegex.IsMatch(data);
    }

    public bool IsValidUser(string username)
    {
        return !string.IsNullOrWhiteSpace(username) && UserRegex().IsMatch(username);
    }

    public bool IsValidPassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) && PasswordRegex().IsMatch(password);
    }

    public bool IsValidNumber(int data)
    {
        return data > 0;
    }

    public bool IsValidDate(DateTime data)
    {
        return data >= DateTime.Now && data <= DateTime.Now.AddMonths(7);
    }

    public bool IsValidYear(int data)
    {
        return data >= 1900 && data <= DateTime.Now.Year;
    }
}