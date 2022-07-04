using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class AddCategoryViewModel : MyNotifyPropChanged
    {
        private AppDBContext _db = new();
        private User _user;

        private CategoryUI _newCategory;
        public CategoryUI NewCategory
        {
            get => _newCategory;
            set => SetProperty(ref _newCategory, value);
        }

        private DelegateCommand _accept;
        public DelegateCommand Accept =>
            _accept ?? (_accept = new DelegateCommand(AddNewCategory));
        private void AddNewCategory()
        {
            var dbCat = CustomMapper.GetInstance().Map<Category>(NewCategory);
            dbCat.UserId = _user.Id;
            _db.Categories.Add(dbCat);
            UserSaver.currentUser.Categories.Add(dbCat);
            _db.SaveChanges();
        }

        public AddCategoryViewModel()
        {
            _user = UserSaver.GetUser();
            NewCategory = new();
        }


    }
}
