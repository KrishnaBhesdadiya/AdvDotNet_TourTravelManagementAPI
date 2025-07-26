using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class PackageDestination
{
    public int PackageDestinationId { get; set; }

    public int PackageId { get; set; }

    public int DestinationId { get; set; }

    public string? OrderInTour { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual MstDestination? Destination { get; set; } = null!;
    [JsonIgnore]
    public virtual MstPackage? Package { get; set; } = null!;
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
