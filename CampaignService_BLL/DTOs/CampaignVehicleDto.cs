using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CampaignService_Service.DTOs
{
    public class CampaignVehicleDto
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public long VehicleId { get; set; }
        public long CampaignId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CampaignDto Campaign { get; set; }
        
        // Ignore collections to prevent circular references
        [JsonIgnore]
        public ICollection<CampaignAppointmentDto>? CampaignAppointments { get; set; }
    }
}
