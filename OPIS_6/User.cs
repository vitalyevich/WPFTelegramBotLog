using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OPIS_6
{
    class User
    {
        static User() { allId = 0; }
        static int allId;
        public User()
        {
            Id = ++allId;
            MessageLog = new ObservableCollection<string>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public ObservableCollection<string>MessageLog { get; set; }

        public override string ToString()
        {
            return this.FirstName;
        }

    }
}
