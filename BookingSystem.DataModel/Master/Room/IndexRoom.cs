﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Room
{
    public class IndexRoom
    {
        public IEnumerable<RowRoomVM> list { get; set; } = new List<RowRoomVM>();
    }
}
