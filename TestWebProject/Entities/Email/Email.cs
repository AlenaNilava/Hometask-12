namespace TestWebProject.Entities.Email
{
    public abstract class Email
    {
        public readonly string address;
        public readonly string subject;
        public readonly string expectedTestBody;
        public Email(string address, string subject, string expectedTestBody)
        {
            this.address = address;
            this.subject = subject;
            this.expectedTestBody = expectedTestBody;
        }
    } 
}
