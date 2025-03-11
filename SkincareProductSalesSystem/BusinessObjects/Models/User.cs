using System.Collections.Generic;
using BusinessObjects.Models;

namespace BusinessObjects.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Address { get; set; }

    public DateOnly? Birthdate { get; set; }

    public int? SkinTypeId { get; set; }

    public int? LoyaltyPoints { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<LoyaltyPoint> LoyaltyPointsNavigation { get; set; } = new List<LoyaltyPoint>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual SkinType? SkinType { get; set; }

    public virtual ICollection<UserSkinTest> UserSkinTests { get; set; } = new List<UserSkinTest>();
}
