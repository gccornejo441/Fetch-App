using System;
using System.Collections.Generic;

namespace Fetch.Model.Entities;

public partial class Location
{
    public int Id { get; set; }

    public int? ShopId { get; set; }

    public string? Address { get; set; }

    public virtual Shop? Shop { get; set; }
}
