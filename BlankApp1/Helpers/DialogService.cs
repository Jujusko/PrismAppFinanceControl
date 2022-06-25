using BlankApp1.ViewModels;
using BlankApp1.Views;
using BlankApp1.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlankApp1.Helpers
{
    public class DialogService : IDIalogService
    {
        static Dictionary<Type, Type> _mappings = new();

        public static void RegisterDialog<TView, TViewModel>()
        {
            try
            {
                _mappings.Add(typeof(TViewModel), typeof(TView));
            }
            catch(Exception ex)
            {

            }
        }

        public void ShowDialog(string name, Action<string> callback)
        {
            var type = Type.GetType($"BlankApp1.Views.Common.{name}");

            ShowDialogInternal(type, callback, null);
        }

        public void ShowDialog<TViewModel>(Action<string> callback)
        {
            var type = _mappings[typeof(TViewModel)];
            ShowDialogInternal(type, callback, typeof(TViewModel));
        }

        private static void ShowDialogInternal(Type type, Action<string> callback, Type vmType)
        {
            var dialog = new Common();

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                callback(dialog.DialogResult.ToString());
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;

            var content = Activator.CreateInstance(type);
            
            if (vmType is not null)
            {
                var vm = Activator.CreateInstance(vmType);
                (content as FrameworkElement).DataContext = vm;
            }

            
            dialog.Content = content;

            dialog.ShowDialog();
        }

        public void ShowDialog(object vm)
        {
            var dialog = new MainWindow((MainWindowViewModel)vm);
            dialog.Show();
        }
    }
}
