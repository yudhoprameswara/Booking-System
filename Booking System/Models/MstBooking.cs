using System;
using System.Collections.Generic;

namespace Booking_System.Models;

public partial class MstBooking
{
    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int Id { get; set; }

    public string? BookCode { get; set; }

    public bool Status { get; set; }
}
