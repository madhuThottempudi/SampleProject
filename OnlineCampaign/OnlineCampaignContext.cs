using Microsoft.EntityFrameworkCore;
using OnlineCampaign.Models;
using System.Threading.Tasks;

namespace OnlineCampaign 
{
    public class OnlineCampaignContext : DbContext
    {
        public OnlineCampaignContext() { }
         public OnlineCampaignContext(DbContextOptions<OnlineCampaignContext> options) : base(options)
        {

        }

        public static bool isMigration = true;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (isMigration)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-HU6GJD4;Database=OnlineCampign;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa; Password=13491a03g3");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Write Fluent API configurations here
            modelBuilder.Entity<Answer>(a =>
            {
                a.Property(x => x.AnswerValue).HasColumnName(nameof(Answer.AnswerValue));
                a.HasOne<Question>().WithMany().HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.ClientSetNull);
                a.HasOne<Option>().WithMany().HasForeignKey(a => a.OptionId).OnDelete(DeleteBehavior.ClientSetNull);

            });

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
