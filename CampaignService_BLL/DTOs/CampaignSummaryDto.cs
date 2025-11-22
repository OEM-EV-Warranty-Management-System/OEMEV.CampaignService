using System;

namespace CampaignService_Service.DTOs
{
    /// <summary>
    /// Simplified Campaign DTO to avoid circular references
    /// </summary>
    public class CampaignSummaryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}