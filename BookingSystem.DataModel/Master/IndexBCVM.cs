using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master
{
    public class IndexBCVM
    {
       public IEnumerable <RowBCVM> list { get; set; } = new List<RowBCVM>();
    }   
}
