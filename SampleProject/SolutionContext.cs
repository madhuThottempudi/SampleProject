using Microsoft.EntityFrameworkCore;
using SampleProject.Models;

namespace SampleProject
{
    public class SolutionContext : DbContext
    {
        public SolutionContext() { }
        public SolutionContext(DbContextOptions<SolutionContext> options) : base(options)
        {

        }

        public static bool isMigration = true;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (isMigration)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-HU6GJD4;Database=CompanyDbName;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa; Password=13491a03g3");

            }
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }

    }
}
