namespace TestWebProject.Entities.Email
{
    class EmptyEmail : Email
    {
        public EmptyEmail(string address, string subject, string expectedTestBody) : base(address, subject, expectedTestBody)
        { }
    }
}
