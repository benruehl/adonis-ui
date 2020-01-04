using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    public interface IMessageBoxViewModel
    {
        string Text { get; set; }

        string Caption { get; set; }

        MessageBoxButtons Buttons { get; set; }

        MessageBoxImage Icon { get; set; }

        MessageBoxResult DefaultResult { get; set; }

        MessageBoxResult Result { get; set; }

        Dictionary<MessageBoxButton, string> CustomButtonLabels { get; set; }
    }
}
