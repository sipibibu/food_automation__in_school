using webAplication.Service.Models;

namespace webAplication.Domain.Accounts
{
    public class Account
    {
        private string id = Guid.NewGuid().ToString();
        private List<Notification> notifications = new List<Notification>();
        public void DeleteNotification()
        {

        }
    }
}
