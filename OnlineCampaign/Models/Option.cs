using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineCampaign.Models
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int OptionId { get; set; }
   
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }

        public Question Question { get; set; }

       
    }
}
