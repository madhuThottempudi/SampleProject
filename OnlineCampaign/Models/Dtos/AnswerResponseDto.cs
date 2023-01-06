namespace OnlineCampaign.Models.Dtos
{
    public class AnswerResponseDto
    {
        public virtual int AnswerId { get; set; }
        public string AnswerValue { get; set; }

        public int QuestionId { get; set; }

        public int OptionId { get; set; }
    }
}
