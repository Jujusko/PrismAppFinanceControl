using BlankApp1.Helpers;
using BlankApp1.ViewModels;
using BlankApp1.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlankApp1.Views.Common
{
    /// <summary>
    /// Interaction logic for Common.xaml
    /// </summary>
    public partial class Common : Window
    {
        public Common()
        {
            InitializeComponent();
            //DialogService.RegisterDialog<MainWindowUC, MainWindowViewModel>();
            //DialogService.RegisterDialog<AddTranzactionUC, AddTranzactionViewModel>();
        }
    }
}
