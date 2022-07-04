using BlankApp1.DataBaseLay;
using BlankApp1.DataBaseLay.Entitys;
using BlankApp1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.LogicPart
{
    public class AnalyticLogic
    {

        private AppDBContext _db;

        private List<Tranzaction> Tranzactions;
        public AnalyticLogic(AppDBContext db)
        {
            _db = db;
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
            var result = Tranzactions
                .Where(d => Convert.ToDateTime(d.Date).Month == DateTime.Now.Month && d.CategoryId == category.Id)
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



    }
}
