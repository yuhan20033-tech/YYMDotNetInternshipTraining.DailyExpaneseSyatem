using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Needed for Include()
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: api/Budget
        [HttpGet]
        public IActionResult GetBudgets()
        {
            var lst = db.Budgets
                .Include(b => b.Category)
                .Include(b => b.User)
                .Select(b => new
                {
                    b.BudgetId,
                    b.UserId,
                    b.CategoryId,
                    b.LimitAmount,
                    b.StartDate,
                    b.EndDate,
                    CategoryName = b.Category != null ? b.Category.CategoryName : null,
                    UserName = b.User != null ? b.User.UserName : null
                })
                .ToList();

            return Ok(lst);
        }

        // GET: api/Budget/5
        [HttpGet("{id}")]
        public IActionResult GetBudgetById(int id)
        {
            var item = db.Budgets
                .Include(b => b.Category)
                .Include(b => b.User)
                .FirstOrDefault(x => x.BudgetId == id);

            if (item == null)
                return NotFound("Budget not found");

            return Ok(new
            {
                item.BudgetId,
                item.UserId,
                item.CategoryId,
                item.LimitAmount,
                item.StartDate,
                item.EndDate,
                CategoryName = item.Category?.CategoryName,
                UserName = item.User?.UserName
            });
        }

        // POST: api/Budget
        [HttpPost]
        public IActionResult CreateBudget(BudgetCreateRequestModel request)
        {
            // Validate Foreign Keys
            if (!db.Users.Any(u => u.UserId == request.UserId))
                return BadRequest($"User with ID {request.UserId} does not exist.");
            if (!db.Categories.Any(c => c.CategoryId == request.CategoryId))
                return BadRequest($"Category with ID {request.CategoryId} does not exist.");

            db.Budgets.Add(new Budget
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                LimitAmount = request.LimitAmount,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            });

            var result = db.SaveChanges();
            return Ok(new BudgetCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Budget created successfully" : "Failed to create budget"
            });
        }

        // PUT: api/Budget/5
        [HttpPut("{id}")]
        public IActionResult UpdateBudget(int id, BudgetUpdateRequestModel request)
        {
            var item = db.Budgets.FirstOrDefault(x => x.BudgetId == id);
            if (item == null)
                return NotFound(new BudgetUpdateResponseModel { IsSuccess = false, Message = "Budget not found" });

            item.UserId = request.UserId;
            item.CategoryId = request.CategoryId;
            item.LimitAmount = request.LimitAmount;
            item.StartDate = request.StartDate;
            item.EndDate = request.EndDate;

            var result = db.SaveChanges();
            return Ok(new BudgetUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Budget updated successfully" : "Failed to update budget",
                
            });
        }

        // PATCH: api/Budget/5
        [HttpPatch("{id}")]
        public IActionResult PatchBudget(int id, BudgetPatchRequestModel request)
        {
            var item = db.Budgets.FirstOrDefault(x => x.BudgetId == id);
            if (item == null)
                return NotFound(new BudgetUpdateResponseModel { IsSuccess = false, Message = "Budget not found" });

            int count = 0;
            if (request.UserId.HasValue) { item.UserId = request.UserId.Value; count++; }
            if (request.CategoryId.HasValue) { item.CategoryId = request.CategoryId.Value; count++; }
            if (request.LimitAmount.HasValue) { item.LimitAmount = request.LimitAmount.Value; count++; }
            if (request.StartDate.HasValue) { item.StartDate = request.StartDate.Value; count++; }
            if (request.EndDate.HasValue) { item.EndDate = request.EndDate.Value; count++; }

            if (count == 0)
                return BadRequest(new BudgetUpdateResponseModel { IsSuccess = false, Message = "No fields to update" });

            var result = db.SaveChanges();
            return Ok(new BudgetUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Budget patched successfully" : "Failed to patch budget",
                
            });
        }

        // DELETE: api/Budget/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBudget(int id)
        {
            var item = db.Budgets.FirstOrDefault(x => x.BudgetId == id);
            if (item == null)
                return NotFound(new BudgetUpdateResponseModel { IsSuccess = false, Message = "Budget not found" });

            db.Budgets.Remove(item);
            var result = db.SaveChanges();
            return Ok(new BudgetUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Budget deleted successfully" : "Failed to delete budget"
            });
        }
    }
}
