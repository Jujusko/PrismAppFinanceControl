using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.DataBaseLay.Entitys
{
    public class RegularTranzaction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
        public int Cost { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
