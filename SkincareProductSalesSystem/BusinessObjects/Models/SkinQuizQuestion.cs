using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;
public partial class SkinQuizQuestion
{
    public int Id { get; set; }

    public string Question { get; set; } = null!;

    public virtual ICollection<SkinQuizAnswer> SkinQuizAnswers { get; set; } = new List<SkinQuizAnswer>();
}
