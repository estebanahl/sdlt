using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("category")]
public class Category
{
    [Column("category_id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; }  = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    public Product Products{get;set;} = null!;
}