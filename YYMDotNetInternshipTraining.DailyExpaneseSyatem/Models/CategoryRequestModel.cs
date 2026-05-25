namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models
{
    public class CategoryCreateRequestModel
    {
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CategoryCreateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class CategoryUpdateRequestModel
    {
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CategoryUpdateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public CategoryModel Data { get; set; } = null!;
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CategoryPatchRequestModel
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }

}
