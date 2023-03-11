using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAplication.Service.Models;

namespace webAplication.Domain.Accounts
{
    abstract class Account
    {
        private string id = Guid.NewGuid().ToString();
        
        private List<Notification> notifications = new List<Notification>();
        public void DeleteNotification()
        {

        }
    }
}
