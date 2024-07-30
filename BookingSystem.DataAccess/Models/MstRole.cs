using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstRole
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? DelBy { get; set; }

    public TimeOnly? DelDate { get; set; }

    public virtual ICollection<MstUser> MstUsers { get; set; } = new List<MstUser>();

    public virtual ICollection<Rolemenu> Rolemenus { get; set; } = new List<Rolemenu>();
}
