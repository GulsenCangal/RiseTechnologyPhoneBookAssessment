using Contact.API.Data;
using Contact.API.Services;
using Contact.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Report.API.Constants;
using Report.API.Data;
using Report.API.Services.Interfaces;
using System.Text;
using ReportRequestData = Report.API.Data.ReportRequestData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportSettings reportSettings;
        private readonly IReportService reportService;

        public ReportController(IReportService reportService, IOptions<ReportSettings> reportSettings)
        {
            this.reportService = reportService;
            this.reportSettings = reportSettings?.Value;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReportReturnData>> CreateNewReport()
        {
            var result = await reportService.CreateNewReport();

            if (result.response == true)
            {
                var model = new ReportRequestData()
                {
                    reportId = ((Report.API.Models.Entity.Report)result.data).uuId
                };
                await reportService.CreateRabbitMQPublisher(model,reportSettings);
                return Accepted("", result);
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
