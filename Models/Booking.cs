using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingCode { get; set; } = null!;

    public int CustomerId { get; set; }

    public int PackageId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime TravelStartDate { get; set; }

    public int NumberOfAdults { get; set; }

    public int NumberOfChildren { get; set; }

    public int TotalBookingPrice { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string BookingStatus { get; set; } = null!;

    public string SpecialRequests { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual MstCustomer Customer { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<MstTraveler> MstTravelers { get; set; } = new List<MstTraveler>();
    [JsonIgnore]
    public virtual MstPackage? Package { get; set; } = null!;
    [JsonIgnore]
    public virtual Payment? Payment { get; set; }
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
