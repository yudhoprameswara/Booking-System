using System;
using System.Collections.Generic;

namespace Booking_System.Models;

public partial class MstResource
{
    public string? ResouceName { get; set; }

    public string? ResourceIcon { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DelBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DelDate { get; set; }

    public int Id { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<MstResourceCode> MstResourceCodes { get; set; } = new List<MstResourceCode>();

    public virtual ICollection<MstRoom> MstRooms { get; set; } = new List<MstRoom>();
}
