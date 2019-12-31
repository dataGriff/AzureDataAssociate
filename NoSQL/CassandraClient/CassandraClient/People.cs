namespace CassandraClient
{
    public class People
    {
        public int userid { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public People(int user_id, string user_name, string user_email)
        {
            this.userid = user_id;
            this.name = user_name;
            this.email = user_email;
        }
    }
}