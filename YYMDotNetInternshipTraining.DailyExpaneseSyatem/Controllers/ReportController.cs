using Microsoft.AspNetCore.Mvc;
using YYMDailyExpanese.Database.AppDbContextModels;
using YYMDotNetInternshipTraining.DailyExpaneseSyatem.Models;

namespace YYMDotNetInternshipTraining.DailyExpaneseSyatem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult GetReports()
        {
            var lst = db.Reports.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetReportById(int id)
        {
            var item = db.Reports.FirstOrDefault(x => x.ReportId == id);
            if (item == null)
                return NotFound("Report not found");

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateReport(ReportCreateRequestModel request)
        {
            db.Reports.Add(new Report
            {
                UserId = request.UserId,
                ReportName = request.ReportName,
                ReportDate = request.ReportDate,
                TotalExpense = request.TotalExpense,
                TotalBudget = request.TotalBudget,
                RemainingBalance = request.RemainingBalance
            });

            var result = db.SaveChanges();
            return Ok(new ReportCreateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Report created successfully" : "Failed to create report"
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReport(int id, ReportUpdateRequestModel request)
        {
            var item = db.Reports.FirstOrDefault(x => x.ReportId == id);
            if (item == null)
                return NotFound(new ReportUpdateResponseModel { IsSuccess = false, Message = "Report not found" });

            item.UserId = request.UserId;
            item.ReportName = request.ReportName;
            item.ReportDate = request.ReportDate;
            item.TotalExpense = request.TotalExpense;
            item.TotalBudget = request.TotalBudget;
            item.RemainingBalance = request.RemainingBalance;

            var result = db.SaveChanges();
            return Ok(new ReportUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Report updated successfully" : "Failed to update report",
                
            });
        }

        [HttpPatch("{id}")]
        public IActionResult PatchReport(int id, ReportPatchRequestModel request)
        {
            var item = db.Reports.FirstOrDefault(x => x.ReportId == id);
            if (item == null)
                return NotFound(new ReportUpdateResponseModel { IsSuccess = false, Message = "Report not found" });

            int count = 0;
            if (request.UserId.HasValue)
            {
                item.UserId = request.UserId.Value;
                count++;
            }
            if (!string.IsNullOrEmpty(request.ReportName))
            {
                item.ReportName = request.ReportName;
                count++;
            }
            if (request.ReportDate.HasValue)
            {
                item.ReportDate = request.ReportDate.Value;
                count++;
            }
            if (request.TotalExpense.HasValue)
            {
                item.TotalExpense = request.TotalExpense.Value;
                count++;
            }
            if (request.TotalBudget.HasValue)
            {
                item.TotalBudget = request.TotalBudget.Value;
                count++;
            }
            if (request.RemainingBalance.HasValue)
            {
                item.RemainingBalance = request.RemainingBalance.Value;
                count++;
            }

            if (count == 0)
                return BadRequest(new ReportUpdateResponseModel { IsSuccess = false, Message = "No fields to update" });

            var result = db.SaveChanges();
            return Ok(new ReportUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Report patched successfully" : "Failed to patch report",
                
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            var item = db.Reports.FirstOrDefault(x => x.ReportId == id);
            if (item == null)
                return NotFound(new ReportUpdateResponseModel { IsSuccess = false, Message = "Report not found" });

            db.Reports.Remove(item);
            var result = db.SaveChanges();
            return Ok(new ReportUpdateResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Report deleted successfully" : "Failed to delete report"
            });
        }
    }
}
