using Microsoft.EntityFrameworkCore;
using TechDataInput.Data;

namespace TechDataInput.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }
        public DbSet<EquipmentGroup> EquipmentGroups { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ParameterDefinition> ParameterDefinitions { get; set; }
        public DbSet<MeasurementSession> MeasurementSessions { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
    }

}
