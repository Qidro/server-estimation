namespace server_estimation.Contracts
{
    public record ComplatingQuestions (string token, int IdSurvey ,int[] IdQiestion, int[] IdAnswer, int[] Level);
}
