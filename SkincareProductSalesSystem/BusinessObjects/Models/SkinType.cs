using BusinessObjects.Models;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class SkinType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SkinQuizAnswer> SkinQuizAnswers { get; set; } = new List<SkinQuizAnswer>();

    public virtual ICollection<UserSkinTest> UserSkinTests { get; set; } = new List<UserSkinTest>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
