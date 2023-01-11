using OnlineCampaign.Enums;

namespace OnlineCampaign.Models.Dtos
{
    public class CreateQuestionDto
    {

        public string Name { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsPublished { get; set; }
    }
}
