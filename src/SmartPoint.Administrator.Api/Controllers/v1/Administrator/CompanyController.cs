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
    [Route("api/v{version:apiVersion}/company")]
    public class CompanyController : MainController
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompanyController(INotificator notificator, ICompanyApplicationService companyApplicationService)
            : base(notificator)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            var companies = await _companyApplicationService.GetCompaniesAsync();

            return CustomResponse(HttpStatusCode.OK, companies);
        }

        [HttpGet]
        [Route("get-by-id-only-active/{id:Guid}")]
        public async Task<IActionResult> GetCompanyByIdOnyActiveAsync(Guid id)
        {
            var company = await _companyApplicationService.GetCompanyByIdOnlyActiveAsync(id);

            return CustomResponse(HttpStatusCode.OK, company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacationAsync(CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);
                return CustomResponse();
            }

            await _companyApplicationService.CreateAsync(request);

            return CustomResponse(HttpStatusCode.Created, request);
        }

        [HttpPut]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> UpdateCompanyAsync(Guid id, UpdateCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);
                return CustomResponse();
            }

            if (!id.Equals(request.Id)) return CustomResponse(HttpStatusCode.Conflict, "Identificador da empresa diverge.");

            await _companyApplicationService.UpdateAsync(request);

            return CustomResponse(HttpStatusCode.OK, request);
        }

        [HttpDelete]
        [Route("id/{id:Guid}")]
        public async Task<IActionResult> RemoveCompanyAsync(Guid id)
        {
            await _companyApplicationService.DeleteAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
