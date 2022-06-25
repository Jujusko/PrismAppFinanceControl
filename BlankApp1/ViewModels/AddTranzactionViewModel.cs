using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class AddTranzactionViewModel : BindableBase
    {

        private AppDBContext _db = new();
        private User _user ;
        private ObservableCollection<CategoryUI> _categories;
        public ObservableCollection<CategoryUI> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private TranzactionUI _tranzToAdd;
        public TranzactionUI TranzToAdd
        {
            get => _tranzToAdd;
            set => SetProperty(ref _tranzToAdd, value);
        }

        private DelegateCommand _accept;
        public DelegateCommand Accept =>
            _accept ?? (_accept = new DelegateCommand(AddTranzaction));
        private void AddTranzaction()
        {
            Tranzaction tranzToAdd = CustomMapper.GetInstance().Map<Tranzaction>(_tranzToAdd);
            tranzToAdd.UserId = _user.Id;
            _db.Tranzactions.Add(tranzToAdd);
            var dbUser = _db.Users.Single(x => x.Id == tranzToAdd.UserId);
            dbUser.Balance -= tranzToAdd.Cost;
            _db.SaveChanges();
            UserSaver.GetUser(dbUser);
        }
        public AddTranzactionViewModel()
        {
            _tranzToAdd = new(100,"Что купили?");
            _user = UserSaver.GetUser();
            //var categoriesFromDb
        }
    }
}
