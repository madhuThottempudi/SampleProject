using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }

        [ForeignKey("Answer")]
        public int QuestionRefId { get; set; }
        public Answer Answer { get; set; }

    }
}
