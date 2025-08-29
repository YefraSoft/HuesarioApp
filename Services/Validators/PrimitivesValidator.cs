using System.Text.RegularExpressions;
using HuesarioApp.Interfaces.DataServices;

namespace HuesarioApp.Services.Validators;

public partial class PrimitivesValidator : IValidator
{
    private readonly Regex _stringRegex = StringRegex();

    [GeneratedRegex(@"^([a-zA-Z\s]{1,50}|[1-9]\.[1-9])$")]
    private static partial Regex StringRegex();

    public bool IsValidString(string data)
    {
        return !string.IsNullOrWhiteSpace(data) && _stringRegex.IsMatch(data);
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
        return data >= 2024 && data <= DateTime.Now.Year;
    }
}