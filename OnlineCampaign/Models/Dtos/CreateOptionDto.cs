using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCampaign.Models.Dtos
{
    public class CreateOptionDto
    {
       // public virtual int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
    }
}
