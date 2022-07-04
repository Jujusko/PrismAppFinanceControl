using BlankApp1.DataBaseLay;
using BlankApp1.Helpers;
using BlankApp1.LogicPart;
using BlankApp1.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlankApp1.DataBaseLay.Entitys;

namespace BlankApp1.ViewModels.TabItems
{
    public class AnalyticViewModel : BindableBase
    {

        private double _monthlyAvgPayment;
        public double MonthlyAvgPayment
        {
            get => _monthlyAvgPayment;
            set => SetProperty(ref _monthlyAvgPayment, value);
        }

        private double _predictSum;
        public double PredictSum
        {
            get => _predictSum;
            set => SetProperty(ref _predictSum, value);
        }

        private CategoryUI _selectedCategory;
        public CategoryUI SelectedCategory
        {
            get => _selectedCategory;
            set 
            {
                SetProperty(ref _selectedCategory, value);
                CalcMonthlyForCategory(_selectedCategory.Name);
                CalcPredictSum(_selectedCategory.Name);
                ChangingCategory();
            }
        }

        private ObservableCollection<TranzactionUI> _tranzactionsByCategory;

        public ObservableCollection<TranzactionUI> TranzactionsByCategory
        {
            get => _tranzactionsByCategory;
            set => SetProperty(ref _tranzactionsByCategory, value);
        }


        private ObservableCollection<CategoryUI> _categories;
        public ObservableCollection<CategoryUI> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private AppDBContext _db = new();

        private AnalyticLogic _logicPart;

        public AnalyticViewModel()
        {
            _logicPart = new(_db);
            TranzactionsByCategory = new();
            Categories = new();
            var categories = _db.Categories.Include(x => x.Tranzactions).Where(x => x.UserId == UserSaver.currentUser.Id).ToList<Category>();
            foreach(var cat in categories)
                Categories.Add(CustomMapper.GetInstance().Map<CategoryUI>(cat));

        }

        private void ChangingCategory()
        {
            TranzactionsByCategory.Clear();
            var tranzactions = _db.Tranzactions.Where(x => x.CategoryId == SelectedCategory.Id && x.UserId == UserSaver.currentUser.Id);
            foreach(var tr in tranzactions)
            {
                TranzactionsByCategory.Add(CustomMapper.GetInstance().Map<TranzactionUI>(tr));
            }
        }

        #region Monthly sum
        private DelegateCommand _getMonthlyCommand;
        public DelegateCommand GetMonthlyCommand
            => _getMonthlyCommand ?? (_getMonthlyCommand = new DelegateCommand(CalcMonthly));

        private DelegateCommand<string> _getMonthlyPaymentsByCategory;
        public DelegateCommand<string> GetMonthlyPaymentsByCategory
            => _getMonthlyPaymentsByCategory ?? (_getMonthlyPaymentsByCategory = new DelegateCommand<string>(CalcMonthlyForCategory));

        private void CalcMonthlyForCategory(string obj)
        {
            if (obj is not null)
            {
                var category = _db.Categories.Single(x => x.Name == obj && x.UserId == UserSaver.currentUser.Id);
                MonthlyAvgPayment = Math.Round (_logicPart.GetMonthlyAvgPayment(category), 2);
            }
            else
                CalcMonthly();
        }

        private void CalcMonthly()
        {
             MonthlyAvgPayment = Math.Round(_logicPart.GetMonthlyAvgPayment(), 2);
        }
        #endregion

        #region Command to predict sum
        private DelegateCommand<string> _getPredictCommon;
        public DelegateCommand<string> GetPredictCommon
            => _getPredictCommon ?? (_getPredictCommon = new DelegateCommand<string>(CalcPredictSum));

        private void CalcPredictSum(string obj)
        {
            if (obj is null)
                PredictSum = _logicPart.PredictableSum();
            else
            {
                var category = _db.Categories.Single(x => x.Name == obj && x.UserId == UserSaver.currentUser.Id);
                PredictSum = Math.Round(_logicPart.PredictableSum(category), 2);
            }
        }
        #endregion
    }
}
