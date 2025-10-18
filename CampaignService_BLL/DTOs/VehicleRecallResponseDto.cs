using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.DTOs
{
    public class VehicleRecallResponseDto
    {
        public long VehicleId { get; set; }
        public List<CampaignDto> ActiveCampaigns { get; set; } = new();
        public bool HasRecall => ActiveCampaigns.Count > 0;
    }
}
