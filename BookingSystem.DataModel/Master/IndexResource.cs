using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master
{
    public class IndexResource
    {
        public IEnumerable<RowResource> Rows { get; set; }= new List<RowResource>();
    }
}
