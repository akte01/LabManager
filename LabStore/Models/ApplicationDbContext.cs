using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LabStore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ConcentrationUnit> ConcentrationUnits { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentLocation> EquipmentLocations { get; set; }
        public DbSet<EquipmentOrder> EquipmentOrders { get; set; }
        public DbSet<SolidOrder> SolidOrders { get; set; }
        public DbSet<SolutionOrder> SolutionOrders { get; set; }
        public DbSet<OrderBase> OrderBases { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}