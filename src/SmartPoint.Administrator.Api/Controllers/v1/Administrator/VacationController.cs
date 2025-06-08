using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;

namespace SmartPoint.Administrator.Api.Controllers.v1.Administrator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/vacation")]
    public class VacationController : ControllerBase
    {
        private readonly IVacationApplicationService _vacationApplicationService;

        public VacationController(IVacationApplicationService vacationApplicationService)
        {
            _vacationApplicationService = vacationApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVacationsAsync()
        {
            var vacations = await _vacationApplicationService.GetVacationsAsync();

            if (vacations == null) return BadRequest();

            return Ok(vacations);
        }

        [HttpGet]
        [Route("get-by-id/{id:Guid}")]
        public async Task<IActionResult> GetVacationByIdAsync(Guid id)
        {
            var vacation = await _vacationApplicationService.GetVacationByIdAsync(id);

            if (vacation == null) return BadRequest();

            return Ok(vacation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacationAsync(CreateVacationRequest request)
        {
            await _vacationApplicationService.CreateAsync(request);

            return Ok(request);
        }

        [HttpPut]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> UpdateVacationAsync(Guid id, UpdateVacationRequest request)
        {
            if (!id.Equals(request.Id)) return BadRequest();

            await _vacationApplicationService.UpdateAsync(request);

            return Ok(request);
        }

        [HttpDelete]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> RemoveVacationAsync(Guid id)
        {
            await _vacationApplicationService.DeleteAsync(id);

            return Ok();
        }
    }
}
