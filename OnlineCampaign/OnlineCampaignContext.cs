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
            //modelBuilder.Entity<Answer>
            //    (m => {
            //        m.Property(x => x.AnswerValue).HasColumnName(nameof(Answer.AnswerValue));
              
            //    m.HasOne<Option>().WithMany().HasForeignKey(m => m.OptionId).OnDelete(DeleteBehavior.ClientSetNull);
            //        m.Property(p => new { p.OptionId, p.QuestionId, p.QuestionType, p.Option.OptionName, p.Option.OptionValue });
            //        m.ToTable();

            //}).Map(m =>
            //{
            //    m.Properties(p => new { p.Id, p.Name, p.QuestionType, p.IsPublished});
            //    m.HasOne<Question>(Question.QuestionId).WithMany().HasForeignKey(m => m.QuestionId).OnDelete(DeleteBehavior.ClientSetNull);
            //    m.ToTable();

            //});

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Answer> Answers { get; set; }
      
    }
}
