using SQLite;

namespace HuesarioApp.Models.Entities;

[Table("parts")]
public class Parts
{
    public Parts()
    {
        CreatedAt = DateTime.Now;
    }
    
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("side")]
    public string Side { get; set; } // LEFT, RIGHT, etc.

    [Column("part_category")]
    public string PartCategory { get; set; } // COLLISION, CHASSIS, etc.

    [Column("price")]
    public decimal Price { get; set; }

    [Column("model_id")]
    public int ModelId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}