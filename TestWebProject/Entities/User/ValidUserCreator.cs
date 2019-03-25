namespace TestWebProject.Entities.User
{
    class ValidUserCreator : UserCreator
    {
        public ValidUserCreator() // constructor
        {

        }

        public override User Create(string login, string password)
        {
            return new ValidUser(login, password);
        }
    }
}
