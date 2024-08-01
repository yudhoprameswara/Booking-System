using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Room
{
    public class RowRoomVM
    {
        public string name {  get; set; }
        public int roomId { get; set; }
        public int? floor { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public int? capacity { get; set; }

        public string roomColour { get; set; }  
    }
}
