using SQLite;

namespace HuesarioApp.Models.Entities;

[Table("sessions")]
public class Sessions
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("username")]
    public string Username { get; set; }

    [Column("role_name")]
    public string RoleName { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    
    [Column("image")]
    public string Image { get; set; }
}