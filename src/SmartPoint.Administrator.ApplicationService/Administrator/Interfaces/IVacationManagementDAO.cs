namespace SmartPoint.Administrator.ApplicationService.Administrator.Interfaces
{
    public interface IVacationManagementDAO
    {
        Task<IEnumerable<dynamic>?> GetVacationManagementAsync(int startYear, int endYear, Guid? userId);
    }
}
