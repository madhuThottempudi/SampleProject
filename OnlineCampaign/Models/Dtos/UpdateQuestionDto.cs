using OnlineCampaign.Enums;

namespace OnlineCampaign.Models.Dtos
{
    public class UpdateQuestionDto
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsPublished { get; set; }
    }
}
