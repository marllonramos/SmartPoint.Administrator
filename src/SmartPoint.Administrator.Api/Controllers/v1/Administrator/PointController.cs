using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.Api.Shared;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using System.Net;

namespace SmartPoint.Administrator.Api.Controllers.v1.Administrator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/point")]
    public class PointController : MainController
    {
        private readonly IPointApplicationService _pointApplicationService;

        public PointController(INotificator notificator, IPointApplicationService pointApplicationService)
            : base(notificator)
        {
            _pointApplicationService = pointApplicationService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPointsAsync()
        {
            var points = await _pointApplicationService.GetPointsAsync();

            return CustomResponse(HttpStatusCode.OK, points);
        }

        [HttpGet]
        [Route("get-by-userid/{userId:Guid}")]
        public async Task<IActionResult> GetPointByIdAsync(Guid userId)
        {
            var point = await _pointApplicationService.GetPointByIdAsync(userId);

            return CustomResponse(HttpStatusCode.OK, point);
        }

        [HttpGet]
        [Route("get-by-userid/{userId:Guid}/date-start/{dateStart}/date-end/{dateEnd}")]
        public async Task<IActionResult> GetRegistrationHistoryByUserIdAsync(Guid userId, DateOnly dateStart, DateOnly dateEnd, [FromQuery] TimeOnly? timeStart = null, [FromQuery] TimeOnly? timeEnd = null)
        {
            var points = await _pointApplicationService.GetRegistrationHistoryByUserIdAsync(userId, dateStart, dateEnd, timeStart, timeEnd);

            return CustomResponse(HttpStatusCode.OK, points);
        }

        [HttpGet]
        [Route("get-by-report-admin/date-start/{dateStart}/date-end/{dateEnd}")]
        public async Task<IActionResult> GetReportRegistrationAsync(DateOnly dateStart, DateOnly dateEnd, [FromQuery] Guid? userId = null)
        {
            var points = await _pointApplicationService.GetReportRegistrationAsync(dateStart, dateEnd, userId);

            return CustomResponse(HttpStatusCode.OK, points);
        }

        [HttpGet]
        [Route("get-week-point-by-userid/{userId:Guid}")]
        public async Task<IActionResult> GetWeekPointByUserIdAsync(Guid userId)
        {
            var points = await _pointApplicationService.GetWeekPointsByUserIdAsync(userId);

            return CustomResponse(HttpStatusCode.OK, points);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePointAsync(CreatePointRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            await _pointApplicationService.CreateAsync(request);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePointAsync(Guid id, UpdatePointRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            if (!id.Equals(request.Id)) return CustomResponse(HttpStatusCode.Conflict, "Identificador do ponto diverge.");

            await _pointApplicationService.UpdateAsync(request);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemovePointAsync(Guid id)
        {
            await _pointApplicationService.DeleteAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
