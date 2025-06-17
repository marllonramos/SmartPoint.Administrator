using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;

namespace SmartPoint.Administrator.Api.Controllers.v1.Administrator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/point")]
    public class PointController : ControllerBase
    {
        private readonly IPointApplicationService _pointApplicationService;

        public PointController(IPointApplicationService pointApplicationService)
        {
            _pointApplicationService = pointApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPointsAsync()
        {
            var points = await _pointApplicationService.GetPointsAsync();

            if (points == null) return BadRequest();

            return Ok(points);
        }

        [HttpGet]
        [Route("get-by-userid/{userId:Guid}")]
        public async Task<IActionResult> GetPointByIdAsync(Guid userId)
        {
            var point = await _pointApplicationService.GetPointByIdAsync(userId);

            if (point == null) return BadRequest();

            return Ok(point);
        }

        [HttpGet]
        [Route("get-by-userid/{userId:Guid}/date-start/{dateStart}/date-end/{dateEnd}")]
        public async Task<IActionResult> GetRegistrationHistoryByUserIdAsync(Guid userId, DateOnly dateStart, DateOnly dateEnd, [FromQuery] TimeOnly? timeStart = null, [FromQuery] TimeOnly? timeEnd = null)
        {
            var point = await _pointApplicationService.GetRegistrationHistoryByUserIdAsync(userId, dateStart, dateEnd, timeStart, timeEnd);

            if (point == null) return BadRequest();

            return Ok(point);
        }

        [HttpGet]
        [Route("get-by-report-admin/date-start/{dateStart}/date-end/{dateEnd}")]
        public async Task<IActionResult> GetReportRegistrationAsync(DateOnly dateStart, DateOnly dateEnd, [FromQuery] Guid? userId = null)
        {
            var point = await _pointApplicationService.GetReportRegistrationAsync(dateStart, dateEnd, userId);

            if (point == null) return BadRequest();

            return Ok(point);
        }

        [HttpGet]
        [Route("get-week-point-by-userid/{userId:Guid}")]
        public async Task<IActionResult> GetWeekPointByUserIdAsync(Guid userId)
        {
            var point = await _pointApplicationService.GetWeekPointByUserIdAsync(userId);

            if (point == null) return BadRequest();

            return Ok(point);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePointAsync(CreatePointRequest request)
        {
            await _pointApplicationService.CreateAsync(request);

            return Ok(request);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePointAsync(Guid id, UpdatePointRequest request)
        {
            if (!id.Equals(request.Id)) return BadRequest();

            await _pointApplicationService.UpdateAsync(request);

            return Ok(request);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemovePointAsync(Guid id)
        {
            await _pointApplicationService.DeleteAsync(id);

            return Ok();
        }
    }
}
