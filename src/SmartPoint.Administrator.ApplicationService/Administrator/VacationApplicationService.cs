using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.ApplicationService.Shared.Notifications;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Builder;
using SmartPoint.Administrator.Domain.Administrator.Enum;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using System.Net;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class VacationApplicationService : IVacationApplicationService
    {
        private readonly INotificator _notificator;
        private readonly IVacationManagementDAO _vacationDAO;
        private readonly IVacationRepository _vacationRepository;

        public VacationApplicationService(INotificator notificator, IVacationRepository vacationRepository, IVacationManagementDAO vacationDAO)
        {
            _notificator = notificator;
            _vacationDAO = vacationDAO;
            _vacationRepository = vacationRepository;
        }

        public async Task<IEnumerable<Vacation>?> GetVacationsAsync()
        {
            var vacations = await _vacationRepository.GetVacationsAsync();

            if (vacations == null || vacations.Count() == 0)
            {
                _notificator.Handle(new Notification("Não há férias cadastradas.", HttpStatusCode.NotFound));

                return null;
            }

            return vacations;
        }

        public async Task<Vacation?> GetVacationByIdAsync(Guid id)
        {
            var vacation = await _vacationRepository.GetVacationByIdAsync(id);

            if (vacation == null)
            {
                _notificator.Handle(new Notification("Férias não encontrada.", HttpStatusCode.NotFound));

                return null;
            }

            return vacation;
        }

        public async Task<IEnumerable<Vacation>?> GetVacationByUserIdAsync(Guid userId, int startYear, int endYear)
        {
            var vacations = await _vacationRepository.GetVacationByUserIdAsync(userId, startYear, endYear);

            if (vacations == null)
            {
                _notificator.Handle(new Notification("Férias não encontrada.", HttpStatusCode.NotFound));

                return null;
            }

            return vacations;
        }

        public async Task<IEnumerable<dynamic>?> GetVacationsManagementAsync(int startYear, int endYear, Guid? userId, VacationStatus? vacationStatus)
        {
            var vacations = await _vacationDAO.GetVacationManagementAsync(startYear, endYear, userId, vacationStatus);

            if (vacations == null)
            {
                _notificator.Handle(new Notification("Férias não encontrada.", HttpStatusCode.NotFound));

                return null;
            }

            return vacations;
        }

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

        public async Task ApproveVacationAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            if (vacation == null) return;

            vacation.Approve();

            await _vacationRepository.UpdateAsync();
        }

        public async Task RejectVacationAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            if (vacation == null) return;

            vacation.Reject();

            await _vacationRepository.UpdateAsync();
        }

        public async Task CancelVacationAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            if (vacation == null) return;

            vacation.Cancel();

            await _vacationRepository.UpdateAsync();
        }

        public async Task UpdateAsync(UpdateVacationRequest request)
        {
            var vacation = await GetVacationByIdAsync(request.Id);

            if (vacation == null) return;

            vacation.Update(request.StartPeriod, request.EndPeriod, request.Obs, request.Status);

            await _vacationRepository.UpdateAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            if (vacation == null) return;

            await _vacationRepository.DeleteAsync(vacation);
        }
    }
}
