namespace TestWebProject.Entities.User
{
    class InvalidUserCreator : UserCreator
    {
        public InvalidUserCreator() // constructor
        {

        }

        public override User Create(string login, string password)
        {
            return new InvalidUser(login);
        }
    }
}
