using System.Text.RegularExpressions;

namespace HuesarioApp.Interfaces.DataServices;

public interface IValidator
{
    public bool IsValidString(string data);
    public bool IsValidNumber(int data);
    public bool IsValidDate(DateTime data);
    public bool IsValidYear(int data);
}