using System;
using System.Collections.Generic;

namespace BookingSystem.DataAccess.Models;

public partial class MstUser
{
    public string Password { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? RoleId { get; set; }

    public int Id { get; set; }

    public string LoginName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public TimeOnly? DelDate { get; set; }

    public int? DelBy { get; set; }

    public virtual MstUser CreatedByNavigation { get; set; } = null!;

    public virtual MstUser? DelByNavigation { get; set; }

    public virtual ICollection<MstUser> InverseCreatedByNavigation { get; set; } = new List<MstUser>();

    public virtual ICollection<MstUser> InverseDelByNavigation { get; set; } = new List<MstUser>();

    public virtual ICollection<MstUser> InverseUpdatedByNavigation { get; set; } = new List<MstUser>();

    public virtual MstRole? Role { get; set; }

    public virtual MstUser? UpdatedByNavigation { get; set; }
}
