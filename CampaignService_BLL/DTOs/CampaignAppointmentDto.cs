using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.DTOs
{
    public class CampaignAppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public long CampaignVehicleId { get; set; }
        public long ServiceCenterId { get; set; }
        public Guid TechnicianId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CampaignVehicleDto CampaignVehicle { get; set; }
    }
}
