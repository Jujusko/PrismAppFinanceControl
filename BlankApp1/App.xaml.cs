using BlankApp1.DataBaseLay;
using BlankApp1.Views;
using BlankApp1.Views.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Windows;

namespace BlankApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            DatabaseFacade facade = new DatabaseFacade(new AppDBContext());
            facade.EnsureCreated();
            return Container.Resolve<RegisterForm>();
        }
 
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<IDialogService, DialogService>();
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    DatabaseFacade facade = new DatabaseFacade(new AppDBContext());
        //    facade.EnsureCreated();
        //}
    }
}
