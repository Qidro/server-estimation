using System.Security.Cryptography.X509Certificates;

namespace server_estimation.Contracts
{
    public record SurveyContract(string title, string description, int[] idQ ,string[] titleQuestion, string[] descriptionQuestion,int[] level, int[] IdQuestion, string[] question, string[] comment, int[] points);
}
