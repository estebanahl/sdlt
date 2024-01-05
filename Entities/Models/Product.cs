using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
    [Column("price")]
    public decimal Price { get; set; }
    [Column("active")]
    public bool Active { get; set; }
    [Column("image_url")]
    public string ImageUrl { get; set; } = string.Empty;
    [Column("category_id")]
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

}