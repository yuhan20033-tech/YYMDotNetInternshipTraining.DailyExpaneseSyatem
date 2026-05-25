using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult GetCategory()
        {
            var lst = db.Categories.ToList();

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var item = db.Categories.FirstOrDefault(x=> x.CategoryId == id);
            if(item == null)
            {
                return NotFound("There is no Category");

            }
            return Ok(item);
                
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateRequestModel requestModel)
        {
            db.Categories.Add(new Category
            {
                CategoryName = requestModel.CategoryName,
                Description = requestModel.Description
            });
            var result = db.SaveChanges();
            return Ok(new CategoryCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Create success" : "Fail to create"
            });

        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,CategoryUpdateRequestModel requestModel)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
            {
                return NotFound("There is no Category");

            }
            item.CategoryName = requestModel.CategoryName;
            item.Description = requestModel.Description;
            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Update Success" : "Fail to update"
            });

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
            {
                return NotFound("There is no Category");

            }
            db.Categories.Remove(item);
            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Delete Sucess" : "Fail to delete"
            });

        }
        [HttpPatch("{id}")]
        public IActionResult PatchCategory(int id,CategoryUpdateRequestModel requestModel)
        {
            var item = db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (item == null)
            {
                return NotFound("There is no Category");

            }
            item.CategoryName= requestModel.CategoryName;
            item.Description= requestModel.Description;
            var result = db.SaveChanges();
            return Ok(new CategoryUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Patch Sucess" : "Fail to patch"
            });
            int count = 0;
            if (!string.IsNullOrEmpty(requestModel.CategoryName))
            {
                count++;
                item.CategoryName = requestModel.CategoryName;
            }
            if (!string.IsNullOrEmpty(requestModel.Description))
            {
                count++;
                item.Description = requestModel.Description;
            }
                
        }

    }
}
