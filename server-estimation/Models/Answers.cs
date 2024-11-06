namespace server_estimation.Models
{
    public class Answers
    {
        //public Answers(string question, string comment, int points) 
        //{
        
        //}
        public int Id { get; set; }

        public string Question { get; set; }

        public string Comment { get; set; }

        public int Points { get; set; }

        public int QuestionId { get; set; }
        public Question Questions { get; set; }
    }
}
