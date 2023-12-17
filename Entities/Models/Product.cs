using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("product")]
public class Product
{
    [Column("product_id")]
    public Guid Id { get; set; }
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("category_id")]
    public Guid CategoryId { get; set; }
    [Column("active")]
    public bool Active { get; set; }
    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
    public Category Category {get;set;} = null!;

}