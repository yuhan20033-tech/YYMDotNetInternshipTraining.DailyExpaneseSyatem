namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models
{
    public class ExpenseCreateRequestModel
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public string Note { get; set; } = null!;
    }

    public class ExpenseCreateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }

    public class ExpenseUpdateRequestModel
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public string Note { get; set; } = null!;
    }

    public class ExpenseUpdateResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public ExpenseModel Data { get; set; } = null!;
    }

    public class ExpenseModel
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Note { get; set; } = null!;
    }

    public class ExpensePatchRequestModel
    {
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public decimal? Amount { get; set; }
        public DateOnly? ExpenseDate { get; set; }
        public string? Note { get; set; }
    }

}
