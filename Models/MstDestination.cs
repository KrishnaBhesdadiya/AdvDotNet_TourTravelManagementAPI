using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class MstDestination
{
    public int DestinationId { get; set; }

    public string DestinationCode { get; set; } = null!;

    public string DestinationName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<PackageDestination> PackageDestinations { get; set; } = new List<PackageDestination>();
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
