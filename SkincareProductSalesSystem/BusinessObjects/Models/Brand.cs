using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string BrandName { get; set; } = null!;

    public string? CountryCode { get; set; }

    public string? Logo { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
