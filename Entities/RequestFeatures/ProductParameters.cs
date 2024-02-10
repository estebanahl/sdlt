namespace sdlt.Entities.RequestFeatures;

public class ProductParameters : RequestParameters
{
    public ProductParameters() => OrderBy = "name"; 
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; } = decimal.MaxValue;

    
    public bool ValidPriceRange() => (MinPrice >= 0 && MaxPrice >= 0) && (MinPrice < MaxPrice);

    public string? SearchTerm { get; set; }
}