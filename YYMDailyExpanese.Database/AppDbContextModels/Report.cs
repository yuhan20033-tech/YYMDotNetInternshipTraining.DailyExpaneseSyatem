using System;
using System.Collections.Generic;

namespace YYMDailyExpanese.Database.AppDbContextModels;

public partial class Report
{
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public string ReportName { get; set; } = null!;

    public DateOnly ReportDate { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal TotalBudget { get; set; }

    public decimal RemainingBalance { get; set; }

    public virtual User User { get; set; } = null!;
}
