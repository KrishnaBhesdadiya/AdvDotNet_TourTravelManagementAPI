using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class Itinerary
{
    public int ItineraryId { get; set; }

    public int PackageId { get; set; }

    public string DayNumber { get; set; } = null!;

    public string ActivityName { get; set; } = null!;

    public string ActivityDescription { get; set; } = null!;

    public string LocationDetails { get; set; } = null!;

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual MstPackage? Package { get; set; } = null!;
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
