using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Builder;
using SmartPoint.Administrator.Domain.Administrator.Repository;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class VacationApplicationService : IVacationApplicationService
    {
        private readonly IVacationRepository _vacationRepository;

        public VacationApplicationService(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync() => await _vacationRepository.GetVacationsAsync();

        public async Task<Vacation?> GetVacationByIdAsync(Guid id) => await _vacationRepository.GetVacationByIdAsync(id);

        public async Task<IEnumerable<Vacation>?> GetVacationByUserIdAsync(Guid userId, int startYear, int endYear) => await _vacationRepository.GetVacationByUserIdAsync(userId, startYear, endYear);

        public async Task CreateAsync(CreateVacationRequest request)
        {
            var vacation = new VacationBuilder()
                                .WithUserId(request.UserId)
                                .WithCompanyId(request.CompanyId)
                                .WithStartDate(request.StartPeriod)
                                .WithEndDate(request.EndPeriod)
                                .WithObs(request.Obs)
                                .Build();

            await _vacationRepository.CreateAsync(vacation);
        }

        public async Task CancellateVacationAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            vacation?.Cancel();

            await _vacationRepository.CancellateVacationAsync();
        }

        public async Task UpdateAsync(UpdateVacationRequest request)
        {
            var vacation = await GetVacationByIdAsync(request.Id);

            if (vacation == null) throw new Exception("Vacation not found.");

            vacation.Update(request.StartPeriod, request.EndPeriod, request.Obs, request.Status);

            await _vacationRepository.UpdateAsync();
        }

        public async Task DeleteAsync(Guid id) => await _vacationRepository.DeleteAsync(id);
    }
}
