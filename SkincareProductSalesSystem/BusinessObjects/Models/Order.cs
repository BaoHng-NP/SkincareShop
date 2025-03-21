﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
