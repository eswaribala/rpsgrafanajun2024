using Microsoft.EntityFrameworkCore;
using PolicyAPI.Models;

namespace PolicyAPI.Contexts
{
    public class PolicyContext:DbContext
    {

        public PolicyContext(DbContextOptions<PolicyContext> options) 
            : base(options) {

            Database.EnsureCreated();
        }

        public DbSet<PolicyHolder> PolicyHolders {  get; set; } 
        public DbSet<Policy> Policies { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Vehicle>  Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PolicyHolder>()
           .Property(m => m.Gender)
           .HasConversion<string>()
           .HasMaxLength(50);
            modelBuilder.Entity<Vehicle>()
                .Property(v=>v.FuelType)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}
