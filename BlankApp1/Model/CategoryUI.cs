using BlankApp1.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Model
{
    public class CategoryUI : MyNotifyPropChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private ObservableCollection<TranzactionUI> _tranzactions;
        public ObservableCollection<TranzactionUI> Tranzactions
        {
            get => _tranzactions;
            set => SetProperty(ref _tranzactions, value);
        }
    }
}
