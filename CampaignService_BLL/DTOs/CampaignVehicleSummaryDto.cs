using System;

namespace CampaignService_Service.DTOs
{
    /// <summary>
    /// Simplified CampaignVehicle DTO to avoid circular references
    /// </summary>
    public class CampaignVehicleSummaryDto
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public long VehicleId { get; set; }
        public long CampaignId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Use summary instead of full object to prevent loops
        public CampaignSummaryDto Campaign { get; set; }
    }
}