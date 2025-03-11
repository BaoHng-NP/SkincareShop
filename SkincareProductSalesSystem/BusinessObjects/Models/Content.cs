using BusinessObjects.Models;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class Content
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ContentType { get; set; }

    public string? Content1 { get; set; }

    public int? AuthorId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Author { get; set; }
}
