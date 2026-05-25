using Microsoft.AspNetCore.Mvc;
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult GetExpenses()
        {
            var lst = db.Expenses.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetExpenseById(int id)
        {
            var item = db.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            if (item == null)
                return NotFound("Expense not found");

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateExpense(ExpenseCreateRequestModel request)
        {
            db.Expenses.Add(new Expense
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Amount = request.Amount,
                ExpenseDate = request.ExpenseDate,
                Note = request.Note
            });

            var result = db.SaveChanges();
            return Ok(new ExpenseCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Expense created successfully" : "Failed to create expense"
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, ExpenseUpdateRequestModel request)
        {
            var item = db.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            if (item == null)
                return NotFound(new ExpenseUpdateResponseModel { IsSuccess = false, Message = "Expense not found" });

            item.UserId = request.UserId;
            item.CategoryId = request.CategoryId;
            item.Title = request.Title;
            item.Amount = request.Amount;
            item.ExpenseDate = request.ExpenseDate;
            item.Note = request.Note;

            var result = db.SaveChanges();
            return Ok(new ExpenseUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Expense updated successfully" : "Failed to update expense",

            });
        }

        [HttpPatch("{id}")]
        public IActionResult PatchExpense(int id, ExpensePatchRequestModel request)
        {
            var item = db.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            if (item == null)
                return NotFound(new ExpenseUpdateResponseModel { IsSuccess = false, Message = "Expense not found" });

            int count = 0;
            if (request.UserId.HasValue)
            {
                item.UserId = request.UserId.Value;
                count++;
            }
            if (request.CategoryId.HasValue)
            {
                item.CategoryId = request.CategoryId.Value;
                count++;
            }
            if (!string.IsNullOrEmpty(request.Title))
            {
                item.Title = request.Title;
                count++;
            }
            if (request.Amount.HasValue)
            {
                item.Amount = request.Amount.Value;
                count++;
            }
            if (request.ExpenseDate.HasValue)
            {
                item.ExpenseDate = request.ExpenseDate.Value;
                count++;
            }
            if (!string.IsNullOrEmpty(request.Note))
            {
                item.Note = request.Note;
                count++;
            }

            if (count == 0)
                return BadRequest(new ExpenseUpdateResponseModel { IsSuccess = false, Message = "No fields to update" });

            var result = db.SaveChanges();
            return Ok(new ExpenseUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Expense patched successfully" : "Failed to patch expense",
               
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            var item = db.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            if (item == null)
                return NotFound(new ExpenseUpdateResponseModel { IsSuccess = false, Message = "Expense not found" });

            db.Expenses.Remove(item);
            var result = db.SaveChanges();
            return Ok(new ExpenseUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Expense deleted successfully" : "Failed to delete expense"
            });
        }
    }
}
