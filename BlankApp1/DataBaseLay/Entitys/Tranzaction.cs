using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.DataBaseLay.Entitys
{
    public class Tranzaction
    {
        [Key]
        public int Id { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
