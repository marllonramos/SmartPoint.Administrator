using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Builder;
using SmartPoint.Administrator.Domain.Administrator.Repository;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class PointApplicationService : IPointApplicationService
    {
        private readonly IPointRepository _pointRepository;
        private readonly IReportPointDAO _reportPointDAO;

        public PointApplicationService(IPointRepository pointRepository, IReportPointDAO reportPointDAO)
        {
            _pointRepository = pointRepository;
            _reportPointDAO = reportPointDAO;
        }

        public async Task<IEnumerable<Point>> GetPointsAsync() => await _pointRepository.GetPointsAsync();

        public async Task<Point?> GetPointByIdAsync(Guid id) => await _pointRepository.GetPointByIdAsync(id);

        public async Task<IEnumerable<Point>?> GetRegistrationHistoryByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd, TimeOnly? timeStart, TimeOnly? timeEnd)
            => await _pointRepository.GetRegistrationHistoryByUserIdAsync(id, dateStart, dateEnd, timeStart, timeEnd);

        public async Task<IEnumerable<dynamic>?> GetReportRegistrationAsync(DateOnly dateStart, DateOnly dateEnd, Guid? id) => await _reportPointDAO.GetReportRegistrationAsync(dateStart, dateEnd, id);

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

        public async Task<IEnumerable<Point>?> GetWeekPointByUserIdAsync(Guid id)
        {
            var startDate = DateOnly.FromDateTime(GetStartOfWeek());

            var today = DateOnly.FromDateTime(DateTime.Now);

            var result = await _pointRepository.GetWeekPointByUserIdAsync(id, startDate, today);

            return result;
        }

        private DateTime GetStartOfWeek()
        {
            var date = DateTime.Now;

            int diff = date.DayOfWeek - DayOfWeek.Monday;

            if (diff < 0) diff += 7;

            return date.AddDays(-1 * diff).Date;
        }

        public async Task UpdateAsync(UpdatePointRequest request)
        {
            var point = await GetPointByIdAsync(request.Id);

            if (point == null) throw new Exception("Point not found");

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

        public async Task DeleteAsync(Guid id) => await _pointRepository.DeleteAsync(id);
    }
}
