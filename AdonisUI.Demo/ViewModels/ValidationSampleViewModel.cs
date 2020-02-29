using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdonisUI.Demo.Framework;

namespace AdonisUI.Demo.ViewModels
{
    class ValidationSampleViewModel
        : ViewModel
        , IApplicationContentView
    {
        public string Name => "Validation";

        public IApplicationContentView.NavigationGroup Group => IApplicationContentView.NavigationGroup.Samples;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _stringValue;
        public string StringValue
        {
            get => _stringValue;
            set
            {
                if (_stringValue != value)
                {
                    _stringValue = value;

                    ValidateStringValue();

                    RaisePropertyChanged(nameof(StringValue));
                }
            }
        }

        private bool _boolValue;
        public bool BoolValue
        {
            get => _boolValue;
            set
            {
                if (_boolValue != value)
                {
                    _boolValue = value;

                    ValidateBoolValue();

                    RaisePropertyChanged(nameof(BoolValue));
                }
            }
        }

        public void Init()
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
