using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master
{
    public class CreateEditRoomVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int floor {  get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }

        //public IEnumerable<RowResource> RowResources { get; set; } = new List<RowResource>();
        public string colour { get; set; }
    }
}
