using SQLite;

namespace HuesarioApp.Models.Entities;

[Table("Brands")]
public class Brands
{
    public Brands()
    {
        CreatedAt = DateTime.Now;
    }
    
    [PrimaryKey, AutoIncrement] 
    public int Id { get; set; }

    [Unique] 
    [Column("name")] 
    public string Name { get; set; }

    [Column("created_at")] 
    public DateTime CreatedAt { get; set; }
}