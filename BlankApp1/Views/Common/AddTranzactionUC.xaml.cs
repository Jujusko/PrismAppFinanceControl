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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlankApp1.Views.Common
{
    /// <summary>
    /// Interaction logic for AddTranzactionUC.xaml
    /// </summary>
    public partial class AddTranzactionUC : UserControl
    {
        public AddTranzactionUC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wind = this.Parent as Window;

            wind.DialogResult = true;
        }
    }
}
