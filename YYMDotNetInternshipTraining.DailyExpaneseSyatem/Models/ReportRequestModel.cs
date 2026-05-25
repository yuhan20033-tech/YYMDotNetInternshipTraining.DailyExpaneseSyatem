namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models
{
    public class ReportCreateRequestModel
    {
        public int UserId { get; set; }
        public string ReportName { get; set; } = null!;
        public DateOnly ReportDate { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class ReportCreateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class ReportUpdateRequestModel
    {
        public int UserId { get; set; }
        public string ReportName { get; set; } = null!;
        public DateOnly ReportDate { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class ReportUpdateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public ReportModel Data { get; set; } = null!;
    }

    public class ReportModel
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string ReportName { get; set; } = null!;
        public DateOnly ReportDate { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class ReportPatchRequestModel
    {
        public int? UserId { get; set; }
        public string? ReportName { get; set; }
        public DateOnly? ReportDate { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? TotalBudget { get; set; }
        public decimal? RemainingBalance { get; set; }
    }

}
