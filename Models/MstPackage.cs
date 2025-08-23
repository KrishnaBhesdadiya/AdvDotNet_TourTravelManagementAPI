using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class MstPackage
{
    public int PackageId { get; set; }

    public string PackageCode { get; set; } = null!;

    public string PackageName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int DurationDays { get; set; }

    public int DurationNights { get; set; }

    public string AvailabilityStatus { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string IncludedFeatures { get; set; } = null!;

    public string ExcludedFeatures { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string CancellationPolicy { get; set; } = null!;

    public int UserId { get; set; }
    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    [JsonIgnore]
    public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
    [JsonIgnore]
    public virtual ICollection<PackageDestination> PackageDestinations { get; set; } = new List<PackageDestination>();
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
