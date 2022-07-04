using BlankApp1.DataBaseLay.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public class StaticData
    {
        private static Category _savedCategory = new();
        private static Tranzaction _savedTranzaction = new();

        public static Category GetGategory()
        {
            return _savedCategory;
        }

        public static void SetCategory(Category category)
            => _savedCategory = category;

        public static Tranzaction GetTranzaction()
        {
            return _savedTranzaction;
        }

        public static void SetTranzaction(Tranzaction tranzaction)
            => _savedTranzaction = tranzaction;
    }
}
