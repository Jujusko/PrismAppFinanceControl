using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.DataBaseLay.Entitys
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public virtual ICollection<Tranzaction> Tranzactions { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
