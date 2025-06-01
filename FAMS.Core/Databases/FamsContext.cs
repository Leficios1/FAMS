using FAMS.Core.Databases.Configs;
using FAMS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Core.Databases
{
    public class FamsContext : DbContext
    {
        public FamsContext()
        {
        }

        public FamsContext(DbContextOptions<FamsContext> options) : base(options)
        {         
        }

        public DbSet<User>? Users { get; set; }
        
        public DbSet<UserPermission>? UserPermissions { get; set; }
        
        public DbSet<TrainingUnit>? TrainingUnits { get; set; }
        
        public DbSet<TrainingProgramSyllabus>? TrainingProgramSyllabuses { get; set; }
        
        public DbSet<TrainingProgram>? TrainingPrograms { get; set; }
        
        public DbSet<TrainingContent>? TrainingContents { get; set; }
        
        public DbSet<SyllabusObjective>? SyllabusObjectives { get; set; }
        
        public DbSet<Syllabus>? Syllabuses { get; set; }
        
        public DbSet<LearningObjective>? LearningObjectives { get; set; }
        
        public DbSet<Class>? Classes { get; set; }
        
        public DbSet<ClassUser>? ClassUsers { get; set; }

        public DbSet<AssessmentScheme>? AssessmentSchemes { get; set; }
        
        public DbSet<DeliveryType>? DeliveryTypes { get; set; }

        public DbSet<CalendarClass> CalendarClasses { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<FSU> FSU {  get; set; }
        public DbSet<ClassTrainerUnit> ClassTrainerUnits { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=35.186.148.127;Initial Catalog=fasm;User ID=SA;Password=yourStrong1@Password;TrustServerCertificate=true;MultipleActiveResultSets=True;");
        //}
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=FAMS;uid=sa;pwd=12345;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ClassConfiguration().Configure(modelBuilder.Entity<Class>());

            new SyllabusObjectiveConfiguration().Configure(modelBuilder.Entity<SyllabusObjective>());
 
            new TrainingContentConfiguration().Configure(modelBuilder.Entity<TrainingContent>());
            
            new TrainingProgramSyllabusConfiguration().Configure(modelBuilder.Entity<TrainingProgramSyllabus>());
            
            new TrainingUnitConfiguration().Configure(modelBuilder.Entity<TrainingUnit>());
            
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            
            new SyllabusConfiguration().Configure(modelBuilder.Entity<Syllabus>());
            
            new ClassUserConfiguration().Configure(modelBuilder.Entity<ClassUser>());

            new TrainingProgramConfiguration().Configure(modelBuilder.Entity<TrainingProgram>());

            new AssessmentSchemeConfiguration().Configure(modelBuilder.Entity<AssessmentScheme>());

            modelBuilder.Entity<CalendarClass>().HasKey(c=>c.Id);
            modelBuilder.Entity<CalendarClass>().HasOne(x => x.Class).WithMany(x => x.CalendarClasses);

            modelBuilder.Entity<Material>().HasOne(x=>x.TrainingContent).WithMany(x=>x.Materials).HasForeignKey(x=>x.ContentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassTrainerUnit>().HasOne(x => x.TrainingUnit).WithMany(x=>x.TrainerUnits).HasForeignKey(x=>x.UnitCode).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClassTrainerUnit>().HasOne(x=>x.Class).WithMany(x=>x.TrainerUnits).HasForeignKey(x=>x.ClassId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClassTrainerUnit>().HasOne(x => x.Trainer).WithMany(x => x.TrainerUnits).HasForeignKey(x => x.TrainerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Seed();
        }
    }
}
