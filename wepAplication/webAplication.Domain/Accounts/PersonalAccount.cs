namespace webAplication.Domain.Accounts
{
    abstract class PersonalAccount : Account
    {
        protected PersonalAccount(string personId)
        {
            this.personId = personId;
        }
        private string id = Guid.NewGuid().ToString();
        private readonly string personId;
        private string email;
        private bool isEmailVerified;
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void VerifyEmail()
        {
            throw new NotImplementedException();
        }
        public void PasswordRecovery()
        {
            throw new NotImplementedException();
        }
    }
}
