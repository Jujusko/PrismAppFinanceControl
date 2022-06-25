using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class RegisterFormViewModel : BindableBase, ICloseWindow
    {

        private AppDBContext dBContext = new();
        IDIalogService windowHelp = new DialogService();

        public bool CanClose = false;
        private ObservableCollection<UserUI> _users = new();

        public ObservableCollection<UserUI> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        private UserUI _user;
        public UserUI User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        private UserUI _registredUser;
        public UserUI RegistredUser
        {
            get => _registredUser;
            set => SetProperty(ref _user, value);
        }


        public RegisterFormViewModel()
        {
            _user = new();
            CanClose = true;
            var dbUsers = dBContext.Users.Where(x => x.Id != 0).ToArray();
            foreach (var user in dbUsers)
            {
                Users.Add(CustomMapper.GetInstance().Map<UserUI>(user));
            }
        }

        private RelayCommand _acceptCommand;

        public RelayCommand AcceptCommand
        {
            get
            {
                return _acceptCommand ??
                  (_acceptCommand = new RelayCommand(obj =>
                  {
                      var toDb = CustomMapper.GetInstance().Map<User>(_user);
                      var result = dBContext.Users.FirstOrDefault(a => a.Name == _user.Name);
                      if (result == null)
                      {
                          dBContext.Users.Add(toDb);
                          dBContext.SaveChanges();
                      }
                      var tmp = dBContext.Users.FirstOrDefault(a => a.Name == _user.Name);
                      UserSaver.GetUser(tmp);
                      ExecuteShowWindow();
                      Close?.Invoke();
                  }));
            }
        }


        private RelayCommand _acceptForRegistredUser;


        public RelayCommand AcceptForRegistredUser
        {
            get
            {
                return _acceptForRegistredUser ??
                  (_acceptForRegistredUser = new RelayCommand(obj =>
                  {
                      var toDb = CustomMapper.GetInstance().Map<User>(_registredUser);
                      var result = dBContext.Users.FirstOrDefault(a => a.Name == _user.Name);
                      if (result == null)
                      {
                          dBContext.Users.Add(toDb);
                          dBContext.SaveChanges();
                      }
                      var tmp = dBContext.Users.FirstOrDefault(a => a.Name == _user.Name);
                      UserSaver.GetUser(tmp);
                      ExecuteShowWindow();
                      Close?.Invoke();
                  }));
            }
        }



        private void ExecuteShowWindow()
        {
            MainWindowViewModel vm = new();
            windowHelp.ShowDialog(vm);
        }

        public Action Close { get; set; }

        bool ICloseWindow.CanClose()
        {
            if (User.Name != null)
                return true;
            return false;
        }
       
    }
}
