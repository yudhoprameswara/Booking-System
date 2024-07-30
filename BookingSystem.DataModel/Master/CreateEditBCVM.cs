using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master
{
    public class CreateEditBCVM
    {
        public int Id { get; set; }

        public string BookingCode { get; set; }
        public Boolean Status { get; set; }
    }
}
