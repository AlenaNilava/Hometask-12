namespace TestWebProject.Entities.User
{
    abstract class UserCreator
    {
        public UserCreator() // constructor
        {

        }

        abstract public User Create(string login, string password);
        
    }
}
