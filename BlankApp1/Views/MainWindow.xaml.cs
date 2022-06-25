using BlankApp1.Helpers;
using BlankApp1.ViewModels;
using BlankApp1.Views.Common;
using Prism.Commands;
using System;
using System.Windows;

namespace BlankApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(MainWindowViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
