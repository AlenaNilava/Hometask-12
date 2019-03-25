using TestWebProject.forms;

namespace TestWebProject.Entities.Email
{
    class DraftEmail : EmailDecorator
    {
        public DraftEmail(Email email) : base(email)
        {
            new EmailPage().CreateDraftEmail(email);
        }
    }
}
