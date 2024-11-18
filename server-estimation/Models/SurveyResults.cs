namespace server_estimation.Models
{
    public class SurveyResults
    {


        public int Id { get; set; }

        public int UsersId { get; set; }
        public Users Users { get; set; }

        public int QuestionId { get; set; }
        public Question Questions { get; set; }

        public int AnswersId { get; set; }
        public Answers Answers { get; set; }

        public int Points { get; set; }
    }


}
