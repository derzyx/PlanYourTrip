using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;

namespace PlanYourTrip_BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Answers> Answers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Contributors> Contributors { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<TripPlans> TripPlans { get; set; }
        
    }
}
