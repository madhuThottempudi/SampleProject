using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineCampaign.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int AnswerId { get; set; }
        public string AnswerValue { get; set; }

        public int QuestionId { get; set; }

        public int OptionId { get; set; }

    }
}
