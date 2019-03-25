namespace TestWebProject.Entities.User
{
    class ValidUser : User
    {
        public ValidUser(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
