using System.ComponentModel.DataAnnotations;

namespace CampaignService_Service.DTOs
{
    public class UpdateAppointmentStatusDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}