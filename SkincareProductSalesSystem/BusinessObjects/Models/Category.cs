﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
