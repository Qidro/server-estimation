namespace server_estimation.Models
{
    public class Question
    {
        public Question(string TitleQuestion, string Description) {
            titleQuestion = TitleQuestion;
            description = Description;
        }
        public int Id { get; set; }

        public string titleQuestion { get; set; }

        public string description { get; set; }

        public Survey survey { get; set; }
    }
}
