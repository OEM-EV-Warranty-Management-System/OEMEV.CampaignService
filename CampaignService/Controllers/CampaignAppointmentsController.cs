using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignAppointmentsController : ControllerBase
    {
        private readonly ICampaignAppointmentService _campaignAppointmentService;

        public CampaignAppointmentsController(ICampaignAppointmentService campaignAppointmentService)
        {
            _campaignAppointmentService = campaignAppointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAllAppointments()
        {
            var appointments = await _campaignAppointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignAppointmentDto>> GetAppointmentById(long id)
        {
            var appointment = await _campaignAppointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignAppointmentDto>> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = await _campaignAppointmentService.CreateAppointmentAsync(createAppointmentDto);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampaignAppointmentDto>> UpdateAppointment(long id, CreateAppointmentDto updateAppointmentDto)
        {
            var appointment = await _campaignAppointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<CampaignAppointmentDto>> UpdateAppointmentStatus(long id, [FromBody] UpdateAppointmentStatusDto statusDto)
        {
            var existingAppointment = await _campaignAppointmentService.GetAppointmentByIdAsync(id);
            if (existingAppointment == null) return NotFound();

            // Create a new CreateAppointmentDto with the updated status
            var updateDto = new CreateAppointmentDto
            {
                AppointmentDate = existingAppointment.AppointmentDate,
                CampaignVehicleId = existingAppointment.CampaignVehicleId,
                ServiceCenterId = existingAppointment.ServiceCenterId,
                TechnicianId = existingAppointment.TechnicianId,
                Status = statusDto.Status
            };

            var appointment = await _campaignAppointmentService.UpdateAppointmentAsync(id, updateDto);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(long id)
        {
            var result = await _campaignAppointmentService.DeleteAppointmentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("campaign-vehicle/{campaignVehicleId}")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByCampaignVehicleId(long campaignVehicleId)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByCampaignVehicleIdAsync(campaignVehicleId);
            return Ok(appointments);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByVehicleId(long vehicleId)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByVehicleIdAsync(vehicleId);
            return Ok(appointments);
        }

        [HttpGet("service-center/{serviceCenterId}")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByServiceCenterId(long serviceCenterId)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByServiceCenterIdAsync(serviceCenterId);
            return Ok(appointments);
        }

        [HttpGet("technician/{technicianId}")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByTechnicianId(Guid technicianId)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByTechnicianIdAsync(technicianId);
            return Ok(appointments);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByDateRangeAsync(startDate, endDate);
            return Ok(appointments);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<CampaignAppointmentDto>>> GetAppointmentsByStatus(string status)
        {
            var appointments = await _campaignAppointmentService.GetAppointmentsByStatusAsync(status);
            return Ok(appointments);
        }
    }
}
