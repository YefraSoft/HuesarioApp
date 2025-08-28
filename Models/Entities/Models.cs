using SQLite;

namespace HuesarioApp.Models.Entities;

[Table("models")]
public class Models
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("year")]
    public string Year { get; set; }

    [Column("brand_id")]
    public int BrandId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("engine")]
    public string Engine { get; set; }

    [Column("transmission")]
    public string Transmission { get; set; } // Puedes validar que sea AUTOMATIC o STANDARD manualmente
}