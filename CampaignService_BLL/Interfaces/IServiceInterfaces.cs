using CampaignService_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.Interfaces
{
    public interface ICampaignService
    {
        Task<IEnumerable<CampaignDto>> GetAllCampaignsAsync();
        Task<CampaignDto?> GetCampaignByIdAsync(int id);
        Task<CampaignDto> CreateCampaignAsync(CreateCampaignDto createCampaignDto);
        Task<CampaignDto?> UpdateCampaignAsync(int id, CreateCampaignDto updateCampaignDto);
        Task<bool> DeleteCampaignAsync(int id);
        Task<IEnumerable<CampaignDto>> GetActiveCampaignsAsync();
        Task<IEnumerable<CampaignDto>> GetCampaignsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public interface ICampaignVehicleService
    {
        Task<IEnumerable<CampaignVehicleDto>> GetAllCampaignVehiclesAsync();
        Task<CampaignVehicleDto?> GetCampaignVehicleByIdAsync(int id);
        Task<CampaignVehicleDto> AddVehicleToCampaignAsync(CreateCampaignVehicleDto createVehicleDto);
        Task<CampaignVehicleDto?> UpdateCampaignVehicleAsync(int id, CreateCampaignVehicleDto updateVehicleDto);
        Task<bool> RemoveVehicleFromCampaignAsync(int campaignVehicleId);
        Task<IEnumerable<CampaignVehicleDto>> GetCampaignVehiclesByCampaignIdAsync(int campaignId);
        Task<IEnumerable<CampaignVehicleDto>> GetCampaignVehiclesByVehicleIdAsync(long vehicleId);
        Task<VehicleRecallResponseDto> CheckVehicleRecallAsync(long vehicleId);
        Task<IEnumerable<VehicleRecallResponseDto>> GetVehiclesWithActiveRecallsAsync();
        Task<bool> VehicleExistsInCampaignAsync(long vehicleId, int campaignId);
    }

    public interface ICampaignAppointmentService
    {
        Task<IEnumerable<CampaignAppointmentDto>> GetAllAppointmentsAsync();
        Task<CampaignAppointmentDto?> GetAppointmentByIdAsync(int id);
        Task<CampaignAppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<CampaignAppointmentDto?> UpdateAppointmentAsync(int id, CreateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByCampaignVehicleIdAsync(int campaignVehicleId);
        Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByVehicleIdAsync(long vehicleId);
        Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByServiceCenterIdAsync(long serviceCenterId);
        Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByTechnicianIdAsync(Guid technicianId);
        Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public interface INotificationService
    {
        Task SendRecallNotificationAsync(VehicleRecallResponseDto vehicleRecall);
        Task SendAppointmentReminderAsync(CampaignAppointmentDto appointment);
        Task SendCampaignNotificationAsync(CampaignDto campaign, IEnumerable<long> vehicleIds);
    }
}
