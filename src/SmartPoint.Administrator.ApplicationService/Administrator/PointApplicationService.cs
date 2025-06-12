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

        public PointApplicationService(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<IEnumerable<Point>> GetPointsAsync() => await _pointRepository.GetPointsAsync();

        public async Task<Point?> GetPointByIdAsync(Guid id) => await _pointRepository.GetPointByIdAsync(id);

        public async Task<IEnumerable<Point>?> GetRegistrationHistoryByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd, TimeOnly? timeStart, TimeOnly? timeEnd) 
            => await _pointRepository.GetRegistrationHistoryByUserIdAsync(id, dateStart, dateEnd, timeStart, timeEnd);

        public async Task CreateAsync(CreatePointRequest request)
        {
            var point = new PointBuilder()
                            .WithUserId(request.UserId)
                            .WithCompanyId(request.CompanyId)
                            .WithType(request.Type)
                            .WithObs(request.Obs)
                            .WithLatitude(request.Latitude)
                            .WithLongitude(request.Longitude)
                            .WithLocation(request.Location)
                            .WithOvertime(request.IsOvertime)
                            .WithReasonOvertime(request.ReasonOvertime)
                            .WithUrlPicture(request.UrlPicture)
                            .WithManual(request.IsManual)
                            .Build();

            await _pointRepository.CreateAsync(point);
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
