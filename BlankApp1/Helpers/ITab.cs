using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlankApp1.Helpers
{
    public interface ITab
    {
        string Name { get; set; }
        DelegateCommand CloseCommand { get; }

        event EventHandler CloseRequested;
    }
}
