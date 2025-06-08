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
        [Route("get-by-id/{id:Guid}")]
        public async Task<IActionResult> GetPointByIdAsync(Guid id)
        {
            var point = await _pointApplicationService.GetPointByIdAsync(id);

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
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> UpdatePointAsync(Guid id, UpdatePointRequest request)
        {
            if (!id.Equals(request.Id)) return BadRequest();

            await _pointApplicationService.UpdateAsync(request);

            return Ok(request);
        }

        [HttpDelete]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> RemovePointAsync(Guid id)
        {
            await _pointApplicationService.DeleteAsync(id);

            return Ok();
        }
    }
}
