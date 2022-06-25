using BlankApp1.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class CommonViewModel
    {

        private DelegateCommand _showMain;
        private IDIalogService _mainWindow = new DialogService();
        public DelegateCommand ShowMain =>
            _showMain ?? (_showMain = new DelegateCommand(ShowWindow));


        private void ShowWindow()
        {
            _mainWindow.ShowDialog<MainWindowViewModel>(result =>
            {
                var a = result;
                //Close?.Invoke();
            });
            //Close?.Invoke();
        }
        //public Action Close { get; set; }

        //public bool CanClose()
        //{
        //    return true;
        //}

    }
}
