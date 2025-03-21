using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class SkinQuizAnswer
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string Answer { get; set; } = null!;

    public int? SkinTypeId { get; set; }

    public virtual SkinQuizQuestion? Question { get; set; }

    public virtual SkinType? SkinType { get; set; }
}
