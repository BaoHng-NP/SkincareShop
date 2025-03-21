using BusinessObjects.Models;
using System.Collections.Generic;

public class SkinCareRoutine
{
    public int StepOrder { get; set; }
    public string StepName { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string RoutineType { get; set; }
    // Thêm danh sách sản phẩm đề xuất
    public List<Product> RecommendedProducts { get; set; } = new List<Product>();
}
