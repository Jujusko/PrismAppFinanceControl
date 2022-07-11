using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Model
{
    public class TranzactionWithDateTimeStruct
    {
        public int Id { get; set; }

        public double Cost { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
