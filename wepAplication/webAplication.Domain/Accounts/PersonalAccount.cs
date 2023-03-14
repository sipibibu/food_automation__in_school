namespace webAplication.Domain.Accounts
{
    public abstract class PersonalAccount : Account
    {
        protected PersonalAccount(string personId)
        {
            this.personId = personId;
        }
        public string personId { get; private set; }
        public string userId { get; private set; }
        public string email { get; private set; }
        public bool isEmailVerified { get; private set; }
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
