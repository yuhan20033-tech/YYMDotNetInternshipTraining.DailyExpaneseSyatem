using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Needed for Include()
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: api/Category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var lst = db.Categories
                        .Include(c => c.Budgets)
                        .Include(c => c.Expenses)
                        .ToList();

            return Ok(lst);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var item = db.Categories
                         .Include(c => c.Budgets)
                         .Include(c => c.Expenses)
                         .FirstOrDefault(x => x.CategoryId == id);

            if (item == null)
                return NotFound("Category not found");

            return Ok(item);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateRequestModel request)
        {
            db.Categories.Add(new Category
            {
                CategoryName = request.CategoryName,
                Description = request.Description
            });

            var result = db.SaveChanges();
            return Ok(new CategoryCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Category created successfully" : "Failed to create category"
            });
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryUpdateRequestModel request)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
                return NotFound(new CategoryUpdateResponseModel { IsSuccess = false, Message = "Category not found" });

            item.CategoryName = request.CategoryName;
            item.Description = request.Description;

            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Category updated successfully" : "Failed to update category",
                
            });
        }

        // PATCH: api/Category/5
        [HttpPatch("{id}")]
        public IActionResult PatchCategory(int id, CategoryUpdateRequestModel request)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
                return NotFound(new CategoryUpdateResponseModel { IsSuccess = false, Message = "Category not found" });

            int count = 0;
            if (!string.IsNullOrEmpty(request.CategoryName)) { item.CategoryName = request.CategoryName; count++; }
            if (!string.IsNullOrEmpty(request.Description)) { item.Description = request.Description; count++; }

            if (count == 0)
                return BadRequest(new CategoryUpdateResponseModel { IsSuccess = false, Message = "No fields to update" });

            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Category patched successfully" : "Failed to patch category",
                
            });
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
                return NotFound(new CategoryUpdateResponseModel { IsSuccess = false, Message = "Category not found" });

            db.Categories.Remove(item);
            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Category deleted successfully" : "Failed to delete category"
            });
        }
    }
}
