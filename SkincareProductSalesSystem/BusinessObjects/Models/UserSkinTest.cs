using System;
using System.Collections.Generic;
namespace BusinessObjects.Models;


public partial class UserSkinTest
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? SkinTypeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual SkinType? SkinType { get; set; }

    public virtual User? User { get; set; }
}
