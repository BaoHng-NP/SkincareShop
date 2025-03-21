using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class ProductDetail
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public DateOnly? ManufactureDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Product? Product { get; set; }
}
