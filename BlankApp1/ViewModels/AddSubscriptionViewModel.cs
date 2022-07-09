using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.ViewModels
{
    public class AddSubscriptionViewModel : BindableBase
    {
        private AppDBContext _db = new();

        private string _subscriptionName;
        public string SubscriptionName
        {
            get => _subscriptionName;
            set
            {
                SetProperty(ref _subscriptionName, value);
                SetExecBool();
            }
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                SetProperty(ref _cost, value);
                SetExecBool();
            }
        }


        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                SetProperty(ref _date, value);
                SetExecBool();
            }
        }
        private bool _canExec;
        public bool CanExec
        {
            get => _canExec;
            set
            {
                SetProperty(ref _canExec, value);
                
            }
        }
        private DelegateCommand<string> _accept;
        public DelegateCommand<string> Accept
            => _accept ?? (_accept = new DelegateCommand<string>(AddNewSubscription).ObservesCanExecute(() => CanExec));

        private void SetExecBool()
        {
            if (Cost != 0 && SubscriptionName != null)
                CanExec = true;
            else
                CanExec = false;
        }
        private void AddNewSubscription(string obj)
        {
            RegularTranzaction newSub = new();
            newSub.Cost = Cost;
            newSub.Day = Date.Day;
            newSub.Name = SubscriptionName;
            newSub.UserId = (UserSaver.currentUser.Id);
            _db.RegularTranzactions.Add(newSub);
            _db.SaveChanges();
        }
    }
}
