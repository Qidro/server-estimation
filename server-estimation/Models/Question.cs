namespace server_estimation.Models
{
    public class Question
    {
        //public Question(string titleQuestion, string description) {
        //    TitleQuestion = titleQuestion;
        //    Description = description;
        //}
        public int Id { get; set; }

        public string TitleQuestion { get; set; }

        public string Description { get; set; }

        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
