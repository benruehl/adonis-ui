using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Exposes properties used to configure the appearance and behavior of a <see cref="MessageBox"/>.
    /// </summary>
    public interface IMessageBoxViewModel
    {
        /// <summary>
        /// A <see cref="String"/> that specifies the text to display.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// A <see cref="String"/> that specifies the title bar caption to display.
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// A <see cref="MessageBoxButtons"/> value that specifies which button or buttons to display.
        /// </summary>
        MessageBoxButtons Buttons { get; set; }

        /// <summary>
        /// A <see cref="MessageBoxImage"/> value that specifies the icon to display.
        /// </summary>
        MessageBoxImage Icon { get; set; }

        /// <summary>
        /// A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.
        /// </summary>
        MessageBoxResult DefaultResult { get; set; }

        /// <summary>
        /// A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.
        /// </summary>
        MessageBoxResult Result { get; set; }

        /// <summary>
        /// A <see cref="Dictionary{MessageBoxButton,String}"/> holding mappings between message box buttons and their labels that can be used to override the default labels.
        /// </summary>
        Dictionary<MessageBoxButton, string> CustomButtonLabels { get; set; }

        /// <summary>
        /// Specifies whether a system sound is played when the message box window opens. Which sound is played depends on the <see cref="Icon"/>.
        /// </summary>
        bool IsSoundEnabled { get; set; }
    }
}
