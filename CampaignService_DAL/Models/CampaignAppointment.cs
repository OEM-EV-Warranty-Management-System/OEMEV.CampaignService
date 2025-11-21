using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaignService_Repository.Models;

public partial class CampaignAppointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public long? CampaignVehicleId { get; set; }

    public long? ServiceCenterId { get; set; }

    public Guid? TechnicianId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual CampaignVehicle? CampaignVehicle { get; set; }
}
