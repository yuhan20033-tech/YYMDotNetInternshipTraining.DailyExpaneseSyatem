using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace YYMDailyExpanese.Database.AppDbContextModels
{
    public partial class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateOnly CreatedDate { get; set; }

        // 👇 Prevent cycles in JSON serialization
        [JsonIgnore]
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
