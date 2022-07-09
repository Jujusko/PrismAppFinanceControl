using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Model
{
    public class RegularTranzactionUI : BindableBase
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
        private int _day;
        public int Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }
        private int _cost;
        public int Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }
    }
}
