using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    public class MessageBoxViewModel
        : IMessageBoxViewModel
        , INotifyPropertyChanged
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _caption;

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        private MessageBoxButtons _buttons;

        public MessageBoxButtons Buttons
        {
            get => _buttons;
            set => SetProperty(ref _buttons, value);
        }

        private MessageBoxImage _icon;

        public MessageBoxImage Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private MessageBoxResult _defaultResult;

        public MessageBoxResult DefaultResult
        {
            get => _defaultResult;
            set => SetProperty(ref _defaultResult, value);
        }

        private MessageBoxResult _result;

        public MessageBoxResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private Dictionary<MessageBoxButton, string> _customButtonLabels = new Dictionary<MessageBoxButton, string>();

        public Dictionary<MessageBoxButton, string> CustomButtonLabels
        {
            get => _customButtonLabels;
            set => SetProperty(ref _customButtonLabels, value);
        }

        public void SetCustomButtonLabel(MessageBoxButton button, string label)
        {
            _customButtonLabels[button] = label;
        }

        private bool _isSoundEnabled = true;

        public bool IsSoundEnabled
        {
            get => _isSoundEnabled;
            set => SetProperty(ref _isSoundEnabled, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise an event on <see cref="INotifyPropertyChanged.PropertyChanged"/> to indicate that a property value changed.
        /// </summary>
        /// <param name="propertyName">Name of the changed property value.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanging.PropertyChanging"/> before the storage changes and <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="comparer">An optional comparer to compare the value of <paramref name="storage"/> and <paramref name="value"/>. If <see langword="null"/> is passed, the default comparer will be used.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and events on <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> were raised; otherwise, <see langword="false"/>.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            if ((comparer ?? EqualityComparer<T>.Default).Equals(storage, value))
                return false;

            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
