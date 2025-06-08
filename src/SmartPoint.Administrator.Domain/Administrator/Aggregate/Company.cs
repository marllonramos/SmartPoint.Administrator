using SmartPoint.Administrator.Domain.Shared;

namespace SmartPoint.Administrator.Domain.Administrator.Aggregate
{
    public class Company : Entity
    {
        public Company(string name)
        {
            Name = name;
            Active = true;
            StartDate = DateTime.UtcNow;
            BlockDate = null;
        }

        // EF Core
        protected Company() { }

        public string Name { get; private set; }
        public bool Active { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? BlockDate { get; private set; }

        public void Update(string name, bool active, DateTime? blockDate)
        {
            Name = name;
            Active = active;
            BlockDate = blockDate;
        }
    }
}
