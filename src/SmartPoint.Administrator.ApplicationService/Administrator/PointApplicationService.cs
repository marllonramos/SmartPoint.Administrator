using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.ApplicationService.Shared.Notifications;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Builder;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using System.Net;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class PointApplicationService : IPointApplicationService
    {
        private readonly INotificator _notificator;
        private readonly IPointRepository _pointRepository;
        private readonly IReportPointDAO _reportPointDAO;

        public PointApplicationService(INotificator notificator, IPointRepository pointRepository, IReportPointDAO reportPointDAO)
        {
            _notificator = notificator;
            _pointRepository = pointRepository;
            _reportPointDAO = reportPointDAO;
        }

        public async Task<IEnumerable<Point>?> GetPointsAsync()
        {
            var points = await _pointRepository.GetPointsAsync();

            if (points == null || points.Count() == 0)
            {
                _notificator.Handle(new Notification("Pontos não encontrados.", HttpStatusCode.NotFound));

                return default;
            }

            return points;
        }

        public async Task<Point?> GetPointByIdAsync(Guid id)
        {
            var point = await _pointRepository.GetPointByIdAsync(id);

            if (point == null)
            {
                _notificator.Handle(new Notification("Ponto não encontrado.", HttpStatusCode.NotFound));

                return default;
            }

            return point;
        }

        public async Task<IEnumerable<Point>?> GetRegistrationHistoryByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd, TimeOnly? timeStart, TimeOnly? timeEnd)
        {
            var points = await _pointRepository.GetRegistrationHistoryByUserIdAsync(id, dateStart, dateEnd, timeStart, timeEnd);

            if (points == null || points.Count() == 0)
            {
                _notificator.Handle(new Notification("Pontos não encontrados.", HttpStatusCode.NotFound));

                return default;
            }

            return points;
        }

        public async Task<IEnumerable<dynamic>?> GetReportRegistrationAsync(DateOnly dateStart, DateOnly dateEnd, Guid? id)
        {
            var points = await _reportPointDAO.GetReportRegistrationAsync(dateStart, dateEnd, id);

            if (points == null || points.Count() == 0)
            {
                _notificator.Handle(new Notification("Pontos não encontrados.", HttpStatusCode.NotFound));

                return default;
            }

            return points;
        }

        public async Task<IEnumerable<Point>?> GetWeekPointsByUserIdAsync(Guid id)
        {
            var startDate = DateOnly.FromDateTime(GetStartOfWeek());

            var today = DateOnly.FromDateTime(DateTime.Now);

            var points = await _pointRepository.GetWeekPointsByUserIdAsync(id, startDate, today);

            if (points == null || points.Count() == 0)
            {
                _notificator.Handle(new Notification("Pontos não encontrados.", HttpStatusCode.NotFound));

                return default;
            }

            return points;
        }

        public async Task CreateAsync(CreatePointRequest request)
        {
            var point = new PointBuilder()
                            .WithUserId(request.UserId)
                            .WithCompanyId(request.CompanyId)
                            .WithType(request.Type)
                            .WithRegisterDate(request.RegisterDate)
                            .WithRegisterHours(request.RegisterHours)
                            .WithObs(request.Obs)
                            .WithLatitude(request.Latitude)
                            .WithLongitude(request.Longitude)
                            .WithLocation(request.Location)
                            .WithOvertime(request.IsOvertime)
                            .WithReasonOvertime(request.ReasonOvertime)
                            .WithUrlPicture(request.UrlPicture)
                            .WithManual(request.IsManual)
                            .WithReasonManual(request.ReasonManual)
                            .Build();

            await _pointRepository.CreateAsync(point);
        }

        public async Task UpdateAsync(UpdatePointRequest request)
        {
            var point = await GetPointByIdAsync(request.Id);

            if (point == null) return;

            point.Update(
                request.Type,
                request.Obs,
                request.RegisterDate,
                request.RegisterHours,
                request.IsOvertime,
                request.ReasonOvertime,
                request.IsManual,
                request.ReasonManual
            );

            await _pointRepository.UpdateAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var point = await GetPointByIdAsync(id);

            if (point == null) return;

            await _pointRepository.DeleteAsync(point);
        }

        private DateTime GetStartOfWeek()
        {
            var date = DateTime.Now;

            int diff = date.DayOfWeek - DayOfWeek.Monday;

            if (diff < 0) diff += 7;

            return date.AddDays(-1 * diff).Date;
        }
    }
}
