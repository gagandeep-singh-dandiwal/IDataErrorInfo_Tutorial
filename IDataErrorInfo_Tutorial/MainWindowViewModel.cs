using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataErrorInfo_Tutorial
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                if (string.IsNullOrEmpty(username))
                {
                    AddError(nameof(Username), "Cannot be empty");
                
                }
                else
                {
                    RemoveError(nameof(Username), "Cannot be empty");
                }
                if (username?.Length < 3)
                {
                    AddError(nameof(Username), "Length cannot be less than 3");
                }
                else
                {
                    RemoveError(nameof(Username), "Length cannot be less than 3");
                }
                if (username?.Length > 10)
                {
                    AddError(nameof(Username), "Length cannot be more than 10");
                }
                else
                {
                    RemoveError(nameof(Username), "Length cannot be more than 10");
                }
                RaisepropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisepropertyChanged();
            }
        }
    }
}
