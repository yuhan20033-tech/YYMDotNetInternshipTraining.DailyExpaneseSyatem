using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // 👈 Needed for JsonIgnore

namespace YYMDailyExpanese.Database.AppDbContextModels
{
    public partial class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        // 👇 Prevent cycles in JSON serialization
        [JsonIgnore]
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
