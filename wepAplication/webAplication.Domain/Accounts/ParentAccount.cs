using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Domain.Accounts
{
    public class ParentAccount : PersonalAccount
    {
        public ParentAccount() :base(null){}
        ParentAccount(string personId) : base(personId)
        {
        }
        private string _id = Guid.NewGuid().ToString();
        public string Id { get { return _id; } }


        public double balance { get; private set; }
        public void TopUpBalance()
        {
            throw new NotImplementedException();
        }
        public double GetBalance()
        {
            throw new NotImplementedException();
        }
    }
}
