namespace server_estimation.Models
{
    public class Answers
    {
        public int Id { get; set; }

        public string question { get; set; }

        public string comment { get; set; }

        public int points { get; set; }

        Question questionId { get; set; }
    }
}
