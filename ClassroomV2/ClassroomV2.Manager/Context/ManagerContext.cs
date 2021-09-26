using ClassroomV2.Manager.Entities;
using ClassroomV2.Membership.Entities;
using Microsoft.EntityFrameworkCore;


namespace ClassroomV2.Manager.Context
{
    public class ManagerContext : DbContext, IManagerContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ManagerContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations())
                .HasMany<Classroom>()
                .WithOne(g => g.ApplicationUser);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassUser> ClassUsers { get; set; }
    }
}
