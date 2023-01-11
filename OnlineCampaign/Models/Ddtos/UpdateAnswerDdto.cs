using OnlineCampaign.Models.Dtos;

namespace OnlineCampaign.Models.Ddtos
{
    public class UpdateAnswerDdto
    {
        public virtual int AnswerId { get; set; }
        public string AnswerValue { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
      
    }
}
