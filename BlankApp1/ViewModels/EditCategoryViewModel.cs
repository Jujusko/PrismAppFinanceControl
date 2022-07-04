using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class EditCategoryViewModel : BindableBase
    {
        private AppDBContext _dBContext = new();



        private string _newName;
        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        private DelegateCommand _accept;
        public DelegateCommand Accept =>
            _accept ?? (_accept = new DelegateCommand(AcceptCommand));
        private void AcceptCommand()
        {
            Category category = StaticData.GetGategory();
            category.Name = NewName;
            _dBContext.SaveChanges();
        }
    }
}
