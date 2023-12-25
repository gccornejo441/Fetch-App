using System;
using System.Collections.Generic;

namespace Fetch.Model.Entities;

public partial class Shop
{
    public int Id { get; set; }

    public string? Shop1 { get; set; }

    public string? Specialty { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
