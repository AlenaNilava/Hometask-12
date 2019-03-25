namespace TestWebProject.Entities.User
{
    class InvalidUser : User
    {
        public InvalidUser(string login)
        {
            this.login = login;
            this.password = "invalid";
        }
    }
}
