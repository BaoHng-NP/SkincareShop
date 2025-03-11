using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class Discount
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? DiscountType { get; set; }

    public decimal DiscountValue { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public decimal? MinOrderValue { get; set; }

    public decimal? MaxDiscount { get; set; }

    public DateTime? CreatedAt { get; set; }
}
