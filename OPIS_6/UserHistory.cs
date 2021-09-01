using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPIS_6
{
    class UserHistory
    {
        public Dictionary<long, User> history { get; set; }
        public UserHistory()
        {
            history = new Dictionary<long, User>();
        }

        public void AddMessage(long UserID, string TextMessage, string Username)
        {
            if (history.ContainsKey(UserID)) { history[UserID].MessageLog.Add(TextMessage); }
            else
            {
                history.Add(UserID, new User()
                {
                    FirstName = Username,

                    MessageLog = new ObservableCollection<string>()
                    {
                        TextMessage
                    }
                });
            }
        }

        public string GetID(long UserID)
        {
            return history[UserID].Id.ToString();
        }

        public string GetLog(long UserID)
        {
            string result = String.Empty;

            foreach (var message in history[UserID].MessageLog)
            {
                result += $">> {message}\n";
            }
            return result;
        }

    }
}
