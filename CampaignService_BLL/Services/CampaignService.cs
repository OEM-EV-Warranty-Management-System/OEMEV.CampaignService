using AutoMapper;
using CampaignService_Repository.Interfaces;
using CampaignService_Repository.Models;
using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;

namespace CampaignService_Service.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CampaignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampaignDto>> GetAllCampaignsAsync()
        {
            var campaigns = await _unitOfWork.Campaigns.GetAllAsync();
            return _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<CampaignDto?> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            return _mapper.Map<CampaignDto>(campaign);
        }

        public async Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto createCampaignDto)
        {
            var campaign = _mapper.Map<Campaign>(createCampaignDto);
            campaign.CreatedAt = DateTime.UtcNow;
            campaign.IsActive = true;

            var createdCampaign = await _unitOfWork.Campaigns.AddAsync(campaign);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CampaignDto>(createdCampaign);
        }

        public async Task<CampaignDto?> UpdateCampaignAsync(int id, CreateCampaignDto updateCampaignDto)
        {
            var existingCampaign = await _unitOfWork.Campaigns.GetByIdAsync(id);
            if (existingCampaign == null) return null;

            _mapper.Map(updateCampaignDto, existingCampaign);
            var updatedCampaign = await _unitOfWork.Campaigns.UpdateAsync(existingCampaign);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CampaignDto>(updatedCampaign);
        }

        public async Task<bool> DeleteCampaignAsync(int id)
        {
            var result = await _unitOfWork.Campaigns.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<CampaignDto>> GetActiveCampaignsAsync()
        {
            var campaigns = await _unitOfWork.Campaigns.GetActiveCampaignsAsync();
            return _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }

        public async Task<IEnumerable<CampaignDto>> GetCampaignsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var campaigns = await _unitOfWork.Campaigns.GetCampaignsByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<CampaignDto>>(campaigns);
        }
    }
}
