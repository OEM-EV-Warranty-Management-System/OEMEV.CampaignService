using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignVehiclesController : ControllerBase
    {
        private readonly ICampaignVehicleService _campaignVehicleService;

        public CampaignVehiclesController(ICampaignVehicleService campaignVehicleService)
        {
            _campaignVehicleService = campaignVehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignVehicleDto>>> GetAllCampaignVehicles()
        {
            var vehicles = await _campaignVehicleService.GetAllCampaignVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignVehicleDto>> GetCampaignVehicleById(int id)
        {
            var vehicle = await _campaignVehicleService.GetCampaignVehicleByIdAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignVehicleDto>> AddVehicleToCampaign(CreateCampaignVehicleDto createVehicleDto)
        {
            try
            {
                var vehicle = await _campaignVehicleService.AddVehicleToCampaignAsync(createVehicleDto);
                return CreatedAtAction(nameof(GetCampaignVehicleById), new { id = vehicle.Id }, vehicle);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampaignVehicleDto>> UpdateCampaignVehicle(int id, CreateCampaignVehicleDto updateVehicleDto)
        {
            var vehicle = await _campaignVehicleService.UpdateCampaignVehicleAsync(id, updateVehicleDto);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveVehicleFromCampaign(int id)
        {
            var result = await _campaignVehicleService.RemoveVehicleFromCampaignAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("campaign/{campaignId}")]
        public async Task<ActionResult<IEnumerable<CampaignVehicleDto>>> GetCampaignVehiclesByCampaignId(int campaignId)
        {
            var vehicles = await _campaignVehicleService.GetCampaignVehiclesByCampaignIdAsync(campaignId);
            return Ok(vehicles);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<CampaignVehicleDto>>> GetCampaignVehiclesByVehicleId(long vehicleId)
        {
            var vehicles = await _campaignVehicleService.GetCampaignVehiclesByVehicleIdAsync(vehicleId);
            return Ok(vehicles);
        }

        [HttpGet("recalls/check/{vehicleId}")]
        public async Task<ActionResult<VehicleRecallResponseDto>> CheckVehicleRecall(long vehicleId)
        {
            var result = await _campaignVehicleService.CheckVehicleRecallAsync(vehicleId);
            return Ok(result);
        }

        [HttpGet("recalls/active")]
        public async Task<ActionResult<IEnumerable<VehicleRecallResponseDto>>> GetVehiclesWithActiveRecalls()
        {
            var vehicles = await _campaignVehicleService.GetVehiclesWithActiveRecallsAsync();
            return Ok(vehicles);
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> VehicleExistsInCampaign([FromQuery] long vehicleId, [FromQuery] int campaignId)
        {
            var exists = await _campaignVehicleService.VehicleExistsInCampaignAsync(vehicleId, campaignId);
            return Ok(exists);
        }
    }
}
