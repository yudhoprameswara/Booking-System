﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.DataModel.Master.Dropdown
{
    public class ListDropdown
    {
        public IEnumerable<LocationDropdown>? locationDropdowns {  get; set; } = new List<LocationDropdown>();
    }
}
