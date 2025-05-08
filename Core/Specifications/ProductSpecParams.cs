using System;

namespace Core.Specifications;

public class ProductSpecParams
{
    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands;
        set 
        {
            _brands = [.. value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))];
        }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get => _types;
        set 
        {
            _types = [.. value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))];
        }
    }

    public string? Sort { get; set; }
    
}
