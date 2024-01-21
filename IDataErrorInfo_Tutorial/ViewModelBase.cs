using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IDataErrorInfo_Tutorial
{
    internal class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public Dictionary<string, ObservableCollection<string>> ErrorsInThisViewModel { get; set; } = new Dictionary<string, ObservableCollection<string>>();

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                ErrorsInThisViewModel.TryGetValue(columnName, out var value);
                if (value != null && value.Any())
                {
                    return value[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void AddError(string propertyName, string errorMessage)
        {
            //if the error already exist in ErrosInThisViewModel
            ErrorsInThisViewModel.TryGetValue(propertyName, out var observable);

            //if the property does not exist add it
            if (observable == null || observable.Count == 0)
            {
                ErrorsInThisViewModel.Add(propertyName, new ObservableCollection<string>());
                ErrorsInThisViewModel[propertyName].Add(errorMessage);
            }

            //if the property already exists , add the error message to the list of errors
            else if (observable != null && !observable.Contains(errorMessage))
            {
                observable.Add(errorMessage);
            }
        }

        public void RemoveError(string propertyName, string errorMessage)
        {
            ErrorsInThisViewModel.TryGetValue(propertyName, out var observable);

            if (observable != null && observable.Contains(errorMessage) && observable.Count == 1)
            {
                ErrorsInThisViewModel.Remove(propertyName);
            }
            else if(observable != null && observable.Contains(errorMessage))
            {
                observable.Remove(errorMessage);
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisepropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
