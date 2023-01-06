using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineCampaign.Enums;

namespace OnlineCampaign.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsPublished { get; set; }
    }
}
