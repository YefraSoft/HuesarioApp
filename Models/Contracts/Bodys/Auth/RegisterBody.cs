namespace HuesarioApp.Models.Contracts.Bodys.Auth;

public class RegisterBody
{
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; } 
    public int roleId { get; set; } 
}