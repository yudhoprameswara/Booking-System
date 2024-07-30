using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstRoom
{
    public int RoomId { get; set; }

    public int? Floor { get; set; }

    public string? Description { get; set; }

    public string? LocationOffice { get; set; }

    public int? Capacity { get; set; }

    public string? RoomColor { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }

    public int? ResourceId { get; set; }

    public string? RoomName { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual MstResource? Resource { get; set; }
}
