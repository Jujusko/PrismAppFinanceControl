﻿using BlankApp1.Helpers;
using BlankApp1.ViewModels;
using BlankApp1.Views.Common;
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

namespace BlankApp1.Views
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
            DialogService.RegisterDialog<AddTranzactionUC, AddTranzactionViewModel>();
            DialogService.RegisterDialog<AddCategoryUserControl, AddCategoryViewModel>();
            DialogService.RegisterDialog<EditCategoryUserControl, EditCategoryViewModel>();
            DialogService.RegisterDialog<AddSubscriptionUserControl, AddSubscriptionViewModel>();
        }
    }
}
