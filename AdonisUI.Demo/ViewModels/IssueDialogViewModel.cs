using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdonisUI.Demo.ViewModels
{
    class IssueDialogViewModel
        : ViewModel
    {
        private string _stringValue;
        public string StringValue
        {
            get
            {
                return _stringValue;
            }
            set
            {
                if (_stringValue != value)
                {
                    _stringValue = value;

                    ValidateStringValue();

                    RaisePropertyChanged("StringValue");
                }
            }
        }

        public IssueDialogViewModel()
        {
            ValidateStringValue();
        }

        private void ValidateStringValue()
        {
            ClearValidationErrors("StringValue");

            if (String.IsNullOrEmpty(StringValue))
                AddValidationError("StringValue", "Value must not be null or empty.");
        }
    }
}
