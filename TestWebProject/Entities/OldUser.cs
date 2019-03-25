namespace TestWebProject.Entities
{
    public class OldUser
    {
        public readonly string password;
        public readonly string login;

        public OldUser(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }

    
}
