namespace TestWebProject.Entities.Email
{
    abstract class EmailDecorator : Email 
    {
        public Email email;
        public EmailDecorator(Email email) : base(email.address, email.subject, email.expectedTestBody)
        {
            this.email = email;
        }
    }
}
