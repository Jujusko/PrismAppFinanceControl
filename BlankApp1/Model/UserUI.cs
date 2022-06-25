using BlankApp1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Model
{
    public class UserUI : MyNotifyPropChanged
    {

        private int _id;
        private string _name;
        private double _balance;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public double Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }
    }
}
