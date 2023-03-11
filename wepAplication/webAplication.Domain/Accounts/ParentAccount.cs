using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace webAplication.Domain.Accounts
{
    internal class ParentAccount : PersonalAccount
    {
        ParentAccount(string personId) : base(personId)
        {
        }
        double balance;
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
