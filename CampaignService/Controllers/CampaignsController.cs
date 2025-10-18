using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampaignService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDto>> GetCampaignById(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignDto>> CreateCampaign(CreateCampaignDto createCampaignDto)
        {
            var campaign = await _campaignService.CreateCampaignAsync(createCampaignDto);
            return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.Id }, campaign);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampaignDto>> UpdateCampaign(int id, CreateCampaignDto updateCampaignDto)
        {
            var campaign = await _campaignService.UpdateCampaignAsync(id, updateCampaignDto);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCampaign(int id)
        {
            var result = await _campaignService.DeleteCampaignAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetActiveCampaigns()
        {
            var campaigns = await _campaignService.GetActiveCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetCampaignsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var campaigns = await _campaignService.GetCampaignsByDateRangeAsync(startDate, endDate);
            return Ok(campaigns);
        }
    }
}
