using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;


public partial class UserVoucher
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? DiscountId { get; set; }

    public DateTime? RedeemedAt { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual User? User { get; set; }
}
