using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public interface IDIalogService
    {
        void ShowDialog(string name, Action<string> callback);
        void ShowDialog<ViewModel>(Action<string> callback);
        void ShowDialog(object vm);
    }
}
