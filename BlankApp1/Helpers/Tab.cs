using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public abstract class Tab : ITab
    {
        public string Name { get; set; }

        public DelegateCommand CloseCommand { get; }

        public event EventHandler CloseRequested;

        public Tab()
        {
            CloseCommand = new DelegateCommand(CloseThis);
        }

        private void CloseThis()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
