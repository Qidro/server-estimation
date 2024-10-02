namespace server_estimation.Models
{
    public class Sessions
    {
        public Sessions(int idUser, string numberSession)
        {
            IdUser = idUser;
            NumberSession = numberSession;
        }

        public int Id { get; set; }

        public int IdUser { get; set; }

        public string NumberSession { get; set; }

    }
}
