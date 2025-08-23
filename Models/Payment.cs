using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TourTravel.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public DateTime PaymentDate { get; set; }

    public int Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string TransactionId { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual Booking? Booking { get; set; } = null!;
    [JsonIgnore]
    public virtual MstUser? User { get; set; } = null!;
}
