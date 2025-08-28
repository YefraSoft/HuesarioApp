using SQLite;

namespace HuesarioApp.Models.Entities;

[Table("sales")]
public class Sales
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("part_id")]
    public int PartId { get; set; }

    [Column("custom_part_name")]
    public string CustomPartName { get; set; }
    
    [Column("name_image")]
    public string NameImage { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("payment_method")]
    public string PaymentMethod { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}