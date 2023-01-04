using SampleProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsPublished { get; set; }

        [ForeignKey("Option")]
        public int QuestionRefId { get; set; }
        public Option Option { get; set; }

    }
    
}
