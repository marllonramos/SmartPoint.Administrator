using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;

namespace SmartPoint.Administrator.Api.Controllers.v1.Administrator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompanyController(ICompanyApplicationService companyApplicationService)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            var companies = await _companyApplicationService.GetCompaniesAsync();

            if (companies == null) return BadRequest();

            return Ok(companies);
        }

        [HttpGet]
        [Route("get-by-id/{id:Guid}")]
        public async Task<IActionResult> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyApplicationService.GetCompanyByIdAsync(id);

            if (company == null) return BadRequest();

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacationAsync(CreateCompanyRequest request)
        {
            await _companyApplicationService.CreateAsync(request);

            return Ok(request);
        }

        [HttpPut]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> UpdateCompanyAsync(Guid id, UpdateCompanyRequest request)
        {
            if (!id.Equals(request.Id)) return BadRequest();

            await _companyApplicationService.UpdateAsync(request);

            return Ok(request);
        }

        [HttpDelete]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> RemoveCompanyAsync(Guid id)
        {
            await _companyApplicationService.DeleteAsync(id);

            return Ok();
        }
    }
}
