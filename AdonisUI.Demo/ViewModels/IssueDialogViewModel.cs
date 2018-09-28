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

        private bool _boolValue;
        public bool BoolValue
        {
            get
            {
                return _boolValue;
            }
            set
            {
                if (_boolValue != value)
                {
                    _boolValue = value;

                    ValidateBoolValue();

                    RaisePropertyChanged("BoolValue");
                }
            }
        }

        public IssueDialogViewModel()
        {
            ValidateStringValue();
            ValidateBoolValue();
        }

        private void ValidateStringValue()
        {
            ClearValidationErrors("StringValue");

            if (String.IsNullOrEmpty(StringValue))
                AddValidationError("StringValue", "Value must not be null or empty.");

            if (String.Equals(StringValue, "Error", StringComparison.InvariantCultureIgnoreCase))
                AddValidationError("StringValue", "Value must not equal 'Error'.");
        }

        private void ValidateBoolValue()
        {
            ClearValidationErrors("BoolValue");

            if (!BoolValue)
                AddValidationError("BoolValue", "Value must not be false.");
        }
    }
}
