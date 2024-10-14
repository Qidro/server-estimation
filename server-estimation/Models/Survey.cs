namespace server_estimation.Models
{
    public class Survey
    {
        public Survey(string titleSurvey,string description)
        {
            TitleSurvey = titleSurvey;
            Description = description;
        }
        public int Id { get; set; }

        public string TitleSurvey { get; set; }
        public string Description { get; set; }
    }
}
