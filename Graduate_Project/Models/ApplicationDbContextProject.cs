using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Graduate_Project.Models
{
    public class ApplicationDbContextProject:IdentityDbContext
    {
        public ApplicationDbContextProject(DbContextOptions<ApplicationDbContextProject> options):base(options)
        {
            
        }
        
        public DbSet<GraduationStudent> Students { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<StudentSkill> StudentSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship
            modelBuilder.Entity<StudentSkill>()
                .HasKey(ss => new { ss.StudentId, ss.SkillId });

            modelBuilder.Entity<StudentSkill>()
               .HasOne(ss => ss.Student)
               .WithMany(s => s.StudentSkills)
               .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSkill>()
                .HasOne(ss => ss.Skill)
                .WithMany(s => s.StudentSkills)
                .HasForeignKey(ss => ss.SkillId).IsRequired();

            modelBuilder.Entity<GraduationStudent>()
           .HasOne(s => s.User)
           .WithMany()
           .HasForeignKey(s => s.UserId)
           .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

        
    }
}
