namespace OnlineCampaign.Models.Dtos
{
    public class CreateAnswerDto
    {
        public string AnswerValue { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
    }
}
