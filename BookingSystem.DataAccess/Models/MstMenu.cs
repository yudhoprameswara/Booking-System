using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstMenu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public TimeOnly? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? DelDate { get; set; }

    public int? DelBy { get; set; }

    public virtual ICollection<Rolemenu> Rolemenus { get; set; } = new List<Rolemenu>();
}
