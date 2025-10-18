using AutoMapper;
using CampaignService_Repository.Interfaces;
using CampaignService_Repository.Models;
using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;

namespace CampaignService_Service.Services
{
    public class CampaignVehicleService : ICampaignVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public CampaignVehicleService(
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampaignVehicleDto>> GetAllCampaignVehiclesAsync()
        {
            var vehicles = await _unitOfWork.CampaignVehicles.GetAllAsync();
            return _mapper.Map<IEnumerable<CampaignVehicleDto>>(vehicles);
        }

        public async Task<CampaignVehicleDto?> GetCampaignVehicleByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.CampaignVehicles.GetByIdAsync(id);
            return _mapper.Map<CampaignVehicleDto>(vehicle);
        }

        public async Task<CampaignVehicleDto> AddVehicleToCampaignAsync(CreateCampaignVehicleDto createVehicleDto)
        {
            var exists = await _unitOfWork.CampaignVehicles.VehicleExistsInCampaignAsync(
                createVehicleDto.VehicleId, (int)createVehicleDto.CampaignId);

            if (exists)
            {
                throw new InvalidOperationException("Vehicle already exists in this campaign");
            }

            var campaignVehicle = _mapper.Map<CampaignVehicle>(createVehicleDto);
            campaignVehicle.CreatedAt = DateTime.UtcNow;

            var createdVehicle = await _unitOfWork.CampaignVehicles.AddAsync(campaignVehicle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CampaignVehicleDto>(createdVehicle);
        }

        public async Task<CampaignVehicleDto?> UpdateCampaignVehicleAsync(int id, CreateCampaignVehicleDto updateVehicleDto)
        {
            var existingVehicle = await _unitOfWork.CampaignVehicles.GetByIdAsync(id);
            if (existingVehicle == null) return null;

            _mapper.Map(updateVehicleDto, existingVehicle);
            var updatedVehicle = await _unitOfWork.CampaignVehicles.UpdateAsync(existingVehicle);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CampaignVehicleDto>(updatedVehicle);
        }

        public async Task<bool> RemoveVehicleFromCampaignAsync(int campaignVehicleId)
        {
            var result = await _unitOfWork.CampaignVehicles.DeleteAsync(campaignVehicleId);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<CampaignVehicleDto>> GetCampaignVehiclesByCampaignIdAsync(int campaignId)
        {
            var vehicles = await _unitOfWork.CampaignVehicles.GetByCampaignIdAsync(campaignId);
            return _mapper.Map<IEnumerable<CampaignVehicleDto>>(vehicles);
        }

        public async Task<IEnumerable<CampaignVehicleDto>> GetCampaignVehiclesByVehicleIdAsync(long vehicleId)
        {
            var vehicles = await _unitOfWork.CampaignVehicles.GetByVehicleIdAsync(vehicleId);
            return _mapper.Map<IEnumerable<CampaignVehicleDto>>(vehicles);
        }

        public async Task<VehicleRecallResponseDto> CheckVehicleRecallAsync(long vehicleId)
        {
            var campaignVehicles = await _unitOfWork.CampaignVehicles.GetByVehicleIdAsync(vehicleId);
            var activeCampaignVehicles = campaignVehicles.Where(cv => (bool)cv.Campaign.IsActive);

            var response = new VehicleRecallResponseDto { VehicleId = vehicleId };
            response.ActiveCampaigns = _mapper.Map<List<CampaignDto>>(
                activeCampaignVehicles.Select(cv => cv.Campaign).ToList()
            );

            if (response.HasRecall)
            {
                await _notificationService.SendRecallNotificationAsync(response);
            }

            return response;
        }

        public async Task<IEnumerable<VehicleRecallResponseDto>> GetVehiclesWithActiveRecallsAsync()
        {
            var vehiclesWithRecalls = await _unitOfWork.CampaignVehicles.GetVehiclesWithActiveRecallsAsync();
            var groupedVehicles = vehiclesWithRecalls.GroupBy(cv => cv.VehicleId);

            var response = new List<VehicleRecallResponseDto>();

            foreach (var group in groupedVehicles)
            {
                response.Add(new VehicleRecallResponseDto
                {
                    VehicleId = (long)group.Key,
                    ActiveCampaigns = _mapper.Map<List<CampaignDto>>(
                        group.Select(cv => cv.Campaign).ToList()
                    )
                });
            }

            return response;
        }

        public async Task<bool> VehicleExistsInCampaignAsync(long vehicleId, int campaignId)
        {
            return await _unitOfWork.CampaignVehicles.VehicleExistsInCampaignAsync(vehicleId, campaignId);
        }
    }
}
