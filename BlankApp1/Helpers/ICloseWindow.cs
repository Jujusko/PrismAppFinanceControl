using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp1.Helpers
{
    public interface ICloseWindow
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
