namespace server_estimation.Contracts
{
    public record EditSurveyContract(int Id, string title, string description, int[] idQ, string[] titleQuestion, string[] descriptionQuestion, int[] level, int[] IdQuestion, string[] question, string[] comment, int[] points);
    
}
