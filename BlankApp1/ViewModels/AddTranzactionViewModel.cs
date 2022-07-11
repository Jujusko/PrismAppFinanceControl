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
using System.Windows;

namespace BlankApp1.ViewModels
{
    public class AddTranzactionViewModel : BindableBase
    {

        private AppDBContext _db;
        private User _user ;
        private ObservableCollection<CategoryUI> _categories;
        public ObservableCollection<CategoryUI> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private CategoryUI _selectedCategory;
        public CategoryUI SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        private TranzactionUI _tranzToAdd;
        public TranzactionUI TranzToAdd
        {
            get => _tranzToAdd;
            set => SetProperty(ref _tranzToAdd, value);
        }

        private Visibility _showAdd;
        public Visibility ShowAdd
        {
            get => _showAdd;
            set => SetProperty(ref _showAdd, value);
        }

        private Visibility _editingTranzVisible;
        public Visibility EditTranzVisible
        {
            get => _editingTranzVisible;
            set => SetProperty(ref _editingTranzVisible, value);
        }

        private DateTime _tranzDate;
        public DateTime TranzDate
        {
            get => _tranzDate;
            set => SetProperty(ref _tranzDate, value);
        }

        private DelegateCommand _accept;
        public DelegateCommand Accept =>
            _accept ?? (_accept = new DelegateCommand(AddTranzaction));
        private void AddTranzaction()
        {

            _tranzToAdd.Date = TranzDate.ToShortDateString();
            Tranzaction tranzToAdd = CustomMapper.GetInstance().Map<Tranzaction>(_tranzToAdd);
            if (SelectedCategory is not null)
                tranzToAdd.CategoryId = SelectedCategory.Id;
            else
                tranzToAdd.CategoryId = Categories.Single(x => x.Name == "Default").Id;
            tranzToAdd.UserId = _user.Id;
            _db.Tranzactions.Add(tranzToAdd);
            var dbUser = _db.Users.Single(x => x.Id == tranzToAdd.UserId);
            dbUser.Balance -= tranzToAdd.Cost;
            _db.SaveChanges();
            UserSaver.currentUser.Balance -= tranzToAdd.Cost;
        }
        private DelegateCommand _edit;
        public DelegateCommand Edit
            => _edit ?? (_edit = new DelegateCommand(EditTranzaction));

        private void EditTranzaction()
        {
            var dbUser = _db.Users.Single(x => x.Id == TranzToAdd.UserId);
            dbUser.Balance -= TranzToAdd.Cost;
            _tranzToAdd.Date = TranzDate.ToShortDateString();
            var changedTranz = _db.Tranzactions.Single(x => x.Id == TranzToAdd.Id);
            changedTranz.Name = TranzToAdd.Name;
            changedTranz.Date = TranzToAdd.Date;
            changedTranz.Cost = TranzToAdd.Cost;
            _db.SaveChanges();
        }

        public AddTranzactionViewModel()
        {
            _db = UserSaver.GetDB();
            TranzDate = DateTime.Today;
            var savedTranz = StaticData.GetTranzaction();
            if (savedTranz is null)
            {
                _tranzToAdd = new();
                ShowAdd = Visibility.Visible;
                EditTranzVisible = Visibility.Hidden;
            }
            else
            {
                var dbUser = _db.Users.Single(x => x.Id == savedTranz.UserId);
                dbUser.Balance += savedTranz.Cost;

                _tranzToAdd = savedTranz;

                ShowAdd = Visibility.Hidden;
                EditTranzVisible = Visibility.Visible;

                _db.SaveChanges();
            }
            _user = UserSaver.GetUser();
            Categories = new();
            foreach(var cat in _user.Categories)
            {
                Categories.Add(CustomMapper.GetInstance().Map<CategoryUI>(cat));
            }
        }
    }
}
