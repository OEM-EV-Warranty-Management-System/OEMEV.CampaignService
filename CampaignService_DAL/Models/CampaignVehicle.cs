using System;
using System.Collections.Generic;

namespace CampaignService_Repository.Models;

public partial class CampaignVehicle
{
    public long Id { get; set; }

    public string? Status { get; set; }

    public long? VehicleId { get; set; }

    public long? CampaignId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Campaign? Campaign { get; set; }

    public virtual ICollection<CampaignAppointment> CampaignAppointments { get; set; } = new List<CampaignAppointment>();
}
