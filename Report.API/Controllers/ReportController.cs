using Contact.API.Data;
using Contact.API.Services;
using Contact.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Report.API.Data;
using Report.API.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReportReturnData>> CreateNewReport()
        {
            var result = await reportService.CreateNewReport();

            if (result.response == true)
            {
                return Created("", result);
            }
            else
            {
                return BadRequest(result.message);
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReportReturnData>> GetAllReports()
        {
            var allReports = await reportService.GetAllReports();

            if (allReports.response == false)
            {
                return NotFound(allReports.message);
            }
            else
            {
                return Ok(allReports);
            }
        }
        [HttpGet("{reportId}/Detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReturnData>> GetReportDetail(Guid reportId)
        {
            var result = await reportService.GetReportDetail(reportId);

            if (result.response == false)
            {
                return NotFound(result.message);
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
