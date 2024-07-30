using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master
{
    public class CreateEditResCodVM
    {
        public int Id { get; set; }
        public string? ResourceCode { get; set; }
        public bool status { get; set; }
    }
}
