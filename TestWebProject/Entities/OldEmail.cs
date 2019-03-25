namespace TestWebProject.Entities
{
    public class OldEmail
    {
        public readonly string address;
        public readonly string subject;
        public readonly string expectedTestBody;

        public OldEmail(string address, string subject, string expectedTestBody)
        {
            this.address = address;
            this.subject = subject;
            this.expectedTestBody = expectedTestBody;
        }
    }
}
