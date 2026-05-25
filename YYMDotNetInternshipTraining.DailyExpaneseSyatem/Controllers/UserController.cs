using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Needed for Include()
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = db.Users
                        .Include(u => u.Budgets)
                        .Include(u => u.Expenses)
                        .Include(u => u.Reports)
                        .ToList();

            return Ok(lst);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var item = db.Users
                         .Include(u => u.Budgets)
                         .Include(u => u.Expenses)
                         .Include(u => u.Reports)
                         .FirstOrDefault(x => x.UserId == id);

            if (item == null)
                return NotFound("User not found");

            return Ok(item);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser(UserCreateRequestModel request)
        {
            db.Users.Add(new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                CreatedDate = DateOnly.FromDateTime(request.CreatedDate)
            });

            var result = db.SaveChanges();
            return Ok(new UserCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "User created successfully" : "Failed to create user"
            });
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserUpdateRequestModel request)
        {
            var item = db.Users.FirstOrDefault(x => x.UserId == id);
            if (item == null)
                return NotFound(new UserUpdateResponseModel { IsSuccess = false, Message = "User not found" });

            item.UserName = request.UserName;
            item.Email = request.Email;
            item.Password = request.Password;
            item.CreatedDate = request.CreatedDate;

            var result = db.SaveChanges();
            return Ok(new UserUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "User updated successfully" : "Failed to update user",
                
            });
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var item = db.Users.FirstOrDefault(x => x.UserId == id);
            if (item == null)
                return NotFound(new UserUpdateResponseModel { IsSuccess = false, Message = "User not found" });

            db.Users.Remove(item);
            var result = db.SaveChanges();
            return Ok(new UserUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "User deleted successfully" : "Failed to delete user"
            });
        }
    }
}
