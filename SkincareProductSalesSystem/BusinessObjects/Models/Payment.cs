using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class Payment
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public string? TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order? Order { get; set; }
}
