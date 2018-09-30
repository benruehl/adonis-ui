using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AdonisUI.Demo.ViewModels
{
    class ViewModel
        : INotifyPropertyChanged
        , IDataErrorInfo
    {
        private readonly Dictionary<string, IList<string>> _validationErrors = new Dictionary<string, IList<string>>();

        public string this[string propertyName]
        {
            get
            {
                if (String.IsNullOrEmpty(propertyName))
                    return Error;

                if (_validationErrors.ContainsKey(propertyName))
                    return String.Join(Environment.NewLine, _validationErrors[propertyName]);

                return String.Empty;
            }
        }

        public string Error => String.Join(Environment.NewLine, GetAllErrors());

        private IEnumerable<string> GetAllErrors()
        {
            return _validationErrors.SelectMany(kvp => kvp.Value).Where(e => !String.IsNullOrEmpty(e));
        }

        public void AddValidationError(string propertyName, string errorMessage)
        {
            if (!_validationErrors.ContainsKey(propertyName))
                _validationErrors.Add(propertyName, new List<string>());

            _validationErrors[propertyName].Add(errorMessage);
        }

        public void ClearValidationErrors(string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
