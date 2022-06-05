using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;

namespace PlanYourTrip_BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<TripPlans> TripPlans { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        
    }
}
