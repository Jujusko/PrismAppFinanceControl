using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BlankApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IDIalogService openTranzDS = new DialogService();

        private AppDBContext dBContext = new();

        private UserUI _user;


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
            set
            {
                SetProperty(ref _selectedCategory, value);
                RefreshTranzactionsByCategory();
            }
        }



        private ObservableCollection<TranzactionUI> _tranzactions;
        public ObservableCollection<TranzactionUI> Tranzactions
        {
            get => _tranzactions;
            set => SetProperty(ref _tranzactions, value);
        }
        public UserUI User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        //private TranzactionUI _newTranz;
        //public TranzactionUI NewTranz
        //{
        //    get => _newTranz;
        //    set => SetProperty(ref _newTranz, value);
        //}

        private TranzactionUI _selectedTranz;
        public TranzactionUI SelectedTranz
        {
            get => _selectedTranz;
            set => SetProperty(ref _selectedTranz, value);
        }


        public MainWindowViewModel()
        {
            _user = CustomMapper.GetInstance().Map<UserUI>(UserSaver.GetUser(null));
            _tranzactions = new();
            _selectedTranz = new();
            _categories = new();

            User userId = dBContext.Users.FirstOrDefault(x => x.Name == _user.Name);
            _user.Id = userId.Id;
            _user.Balance = userId.Balance;

            var dbTranzes = dBContext.Tranzactions.Where(x => x.UserId == userId.Id).ToArray();
            foreach (var tr in dbTranzes)
            {
                var uiTranz = CustomMapper.GetInstance().Map<TranzactionUI>(tr);
                _tranzactions.Add(uiTranz);
            }
            var dbCategories = dBContext.Categories.Where(x => x.UserId == userId.Id).ToArray();
            foreach (var tr in dbCategories)
            {
                var uiCat = CustomMapper.GetInstance().Map<CategoryUI>(tr);
                _categories.Add(uiCat);
            }
        }

        private RelayCommand _addTranzactionCommand;

        public RelayCommand AddTranzactionCommand
        {
            get
            {
                return _addTranzactionCommand ??
                  (_addTranzactionCommand = new RelayCommand(obj =>
                  {
                      ExecuteShowWindow();
                  }));

            }
        }
        private RelayCommand _addCategory;

        public RelayCommand AddCategory
        {
            get
            {
                return _addCategory ??
                  (_addCategory = new RelayCommand(obj =>
                  {
                      AddCategoryWindow();
                  }));

            }
        }
        private RelayCommand _removeTranzactionCommand;

        public RelayCommand RemoveTranzactionCommand
        {
            get
            {
                    return _removeTranzactionCommand ??
                      (_removeTranzactionCommand = new RelayCommand(obj =>
                      {

                          var a = dBContext.Tranzactions.Single(x => x.Id == _selectedTranz.Id);
                          dBContext.Tranzactions.Remove(a);
                          dBContext.SaveChanges();
                          Tranzactions.Remove(_selectedTranz);
                      },
                      (obj) => _selectedTranz != null && _selectedTranz.Id != 0));
            }
        }

        private DelegateCommand _editCategory;
        public DelegateCommand EditCategory
            => _editCategory ?? (_editCategory = new DelegateCommand(EditCategoryWindow));


        private DelegateCommand _refresh;
        public DelegateCommand Refresh =>
            _refresh ?? (_refresh = new DelegateCommand(RefreshData));
        private void RefreshData()
        {
            var dbUser = dBContext.Users
                .Include(x => x.Categories)
                .Include(x => x.Tranzactions)
                .Single(x => x.Id == _user.Id);
            _tranzactions.Clear();
            foreach(var t in dbUser.Tranzactions)
            {
                _tranzactions.Add(CustomMapper.GetInstance().Map<TranzactionUI>(t));
            }
            _categories.Clear();
            foreach(var cat in dbUser.Categories)
            {
                _categories.Add(CustomMapper.GetInstance().Map<CategoryUI>(cat));
            }
            UserSaver.GetUser(dbUser);
            User = CustomMapper.GetInstance().Map<UserUI>(UserSaver.GetUser());
        }
        private void EditCategoryWindow()
        {
            openTranzDS.ShowDialog<EditCategoryViewModel>(result =>{ });
        }

        private void ExecuteShowWindow()
        {
            openTranzDS.ShowDialog<AddTranzactionViewModel>(result => { });
            
        }
        private void AddCategoryWindow()
        {
            openTranzDS.ShowDialog<AddCategoryViewModel>(result => { });
        }



        private void RefreshTranzactionsByCategory()
        {
            if (SelectedCategory is not null)
            {
                var tranzByCategory = dBContext.Tranzactions.Where(x => x.CategoryId == SelectedCategory.Id).ToArray();
                Tranzactions.Clear();
                foreach(var tranz in tranzByCategory)
                    Tranzactions.Add(CustomMapper.GetInstance().Map<TranzactionUI>(tranz));
            }
        }

        private RelayCommand _deleteCategory;
        public RelayCommand DeleteCategory
        {
            get
            {
                return _deleteCategory ??
                     (_deleteCategory = new RelayCommand(obj =>
                     {

                         int a = Convert.ToInt32(obj);
                         DeleteCateg(a);
                     }));
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private void DeleteCateg(int id)
        {
            var a = Categories.Single(x => x.Id == id);
            var tranzactionsFromDeletedCategory = dBContext.Tranzactions.Where(x => x.CategoryId == a.Id).ToArray();
            var defaultCategory = dBContext.Categories.SingleOrDefault(x => x.Name == "Default" && x.UserId == _user.Id);
            if (defaultCategory is null)
            {
                defaultCategory = new Category();
                defaultCategory.UserId = _user.Id;
                defaultCategory.Name = "Default";
                dBContext.Categories.Add(defaultCategory);
            }
            foreach (var tranz in tranzactionsFromDeletedCategory)
                tranz.CategoryId = defaultCategory.Id;

            dBContext.Remove(dBContext.Categories.Single(x => x.Id == id));
            dBContext.SaveChanges();

            Categories.Remove(a);
        }
    }
}
