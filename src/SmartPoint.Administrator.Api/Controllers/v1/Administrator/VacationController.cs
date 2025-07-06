using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.Api.Shared;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.Domain.Administrator.Enum;
using System.Net;

namespace SmartPoint.Administrator.Api.Controllers.v1.Administrator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/vacation")]
    public class VacationController : MainController
    {
        private readonly IVacationApplicationService _vacationApplicationService;

        public VacationController(INotificator notificator, IVacationApplicationService vacationApplicationService)
            : base(notificator)
        {
            _vacationApplicationService = vacationApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVacationsAsync()
        {
            var vacations = await _vacationApplicationService.GetVacationsAsync();

            return CustomResponse(HttpStatusCode.OK, vacations);
        }

        [HttpGet]
        [Route("get-by-id/{id:Guid}")]
        public async Task<IActionResult> GetVacationByIdAsync(Guid id)
        {
            var vacation = await _vacationApplicationService.GetVacationByIdAsync(id);

            return CustomResponse(HttpStatusCode.OK, vacation);
        }

        [HttpGet]
        [Route("get-by-userid/{userId:Guid}/date-start/{startYear}/date-end/{endYear}")]
        public async Task<IActionResult> GetVacationByUserIdAsync(Guid userId, int startYear, int endYear)
        {
            var vacations = await _vacationApplicationService.GetVacationByUserIdAsync(userId, startYear, endYear);

            return CustomResponse(HttpStatusCode.OK, vacations);
        }

        [HttpGet]
        [Route("get-vacations-management/start-year/{startYear}/end-year/{endYear}")]
        public async Task<IActionResult> GetVacationsManagementAsync(int startYear, int endYear, [FromQuery] Guid? userId, [FromQuery] int? vacationStatus)
        {
            VacationStatus? status = null;

            if (vacationStatus != null) status = (VacationStatus)vacationStatus;

            var vacations = await _vacationApplicationService.GetVacationsManagementAsync(startYear, endYear, userId, status);

            return CustomResponse(HttpStatusCode.OK, vacations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacationAsync(CreateVacationRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            await _vacationApplicationService.CreateAsync(request);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpPut]
        [Route("approve-vacation")]
        public async Task<IActionResult> ApproveVacationAsync(ApproveVacationRequest request)
        {
            await _vacationApplicationService.ApproveVacationAsync(request.Id);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpPut]
        [Route("reject-vacation")]
        public async Task<IActionResult> RejectVacationAsync(RejectVacationRequest request)
        {
            await _vacationApplicationService.RejectVacationAsync(request.Id);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpPut]
        [Route("cancel-vacation")]
        public async Task<IActionResult> CancellationVacationAsync(CancelVacationRequest request)
        {
            await _vacationApplicationService.CancelVacationAsync(request.Id);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpPut]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> UpdateVacationAsync(Guid id, UpdateVacationRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            if (!id.Equals(request.Id)) return CustomResponse(HttpStatusCode.Conflict, "Identificador das férias diverge.");

            await _vacationApplicationService.UpdateAsync(request);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpDelete]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> RemoveVacationAsync(Guid id)
        {
            await _vacationApplicationService.DeleteAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
