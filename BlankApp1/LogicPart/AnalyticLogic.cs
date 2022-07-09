using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using BlankApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BlankApp1.LogicPart
{
    public class AnalyticLogic
    {

        private AppDBContext _db;

        private List<Tranzaction> Tranzactions;

        public AnalyticLogic()
        {
            _db = UserSaver.GetDB();
            Tranzactions = _db.Tranzactions.Where(x => x.UserId == UserSaver.currentUser.Id).ToList();
        }

        public double GetMonthlyAvgPayment()
        {
            var result = _db.Tranzactions.Where(d => Convert.ToDateTime(d.Date).Month == DateTime.Now.Month).Sum(r => r.Cost);
            result = result / DateTime.Now.Day;
            return result;
        }
        
        public double GetMonthlyAvgPayment(Category category)
        {
            //var result = _db.Tranzactions
                //.Where(d => Convert.ToDateTime(d.Date).Month == DateTime.Now.Month && d.CategoryId == category.Id)
                //.Sum(r => r.Cost);
            var result = GetTranzactionsByCatgory(category)
                .Where(d => Convert.ToDateTime(d.Date).Month == DateTime.Now.Month)
                .Sum(r => r.Cost);
            result = result / DateTime.Now.Day;
            return result;
        }

        public double PredictableSum()
        {
            var avgPayment = GetMonthlyAvgPayment();
            return avgPayment * 30;
        }

        public double PredictableSum(Category category)
        {
            var avgPayment = GetMonthlyAvgPayment(category);
            return avgPayment * 30;
        }

        public double GetTotalSumForLastMonth(Category category)
        {
            var result = GetTranzactionsByCatgory(category)
                .Where(d => Convert.ToDateTime(d.Date).Month == (DateTime.Now.Month - 1))
                .Sum(r => r.Cost);
            return Math.Round(result, 2);
        }

        private List<TranzactionUI> GetTranzactionsByCatgory(Category category)
        {
            var allTranzactions = CustomMapper.GetInstance()
                .Map<List<TranzactionUI>>
                (_db.Tranzactions.Where(d => d.CategoryId == category.Id).ToArray());
            return allTranzactions;
        }

    }
}
