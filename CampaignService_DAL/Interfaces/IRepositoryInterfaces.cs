using CampaignService_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICampaignRepository Campaigns { get; }
        ICampaignVehicleRepository CampaignVehicles { get; }
        ICampaignAppointmentRepository CampaignAppointments { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

    public interface ICampaignRepository
    {
        Task<IEnumerable<Campaign>> GetAllAsync();
        Task<Campaign?> GetByIdAsync(int id);
        Task<Campaign> AddAsync(Campaign campaign);
        Task<Campaign> UpdateAsync(Campaign campaign);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Campaign>> GetActiveCampaignsAsync();
        Task<IEnumerable<Campaign>> GetCampaignsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

    public interface ICampaignVehicleRepository
    {
        Task<IEnumerable<CampaignVehicle>> GetAllAsync();
        Task<CampaignVehicle?> GetByIdAsync(int id);
        Task<CampaignVehicle> AddAsync(CampaignVehicle campaignVehicle);
        Task<CampaignVehicle> UpdateAsync(CampaignVehicle campaignVehicle);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CampaignVehicle>> GetByCampaignIdAsync(int campaignId);
        Task<IEnumerable<CampaignVehicle>> GetByVehicleIdAsync(long vehicleId);
        Task<IEnumerable<CampaignVehicle>> GetVehiclesWithActiveRecallsAsync();
        Task<bool> VehicleExistsInCampaignAsync(long vehicleId, int campaignId);
    }

    public interface ICampaignAppointmentRepository
    {
        Task<IEnumerable<CampaignAppointment>> GetAllAsync();
        Task<CampaignAppointment?> GetByIdAsync(long id);
        Task<CampaignAppointment> AddAsync(CampaignAppointment appointment);
        Task<CampaignAppointment> UpdateAsync(CampaignAppointment appointment);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<CampaignAppointment>> GetByCampaignVehicleIdAsync(long campaignVehicleId);
        Task<IEnumerable<CampaignAppointment>> GetByVehicleIdAsync(long vehicleId);
        Task<IEnumerable<CampaignAppointment>> GetByServiceCenterIdAsync(long serviceCenterId);
        Task<IEnumerable<CampaignAppointment>> GetByTechnicianIdAsync(Guid technicianId);
        Task<IEnumerable<CampaignAppointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<CampaignAppointment>> GetByStatusAsync(string status);
    }

    public interface IServiceProviders
    {
        // Common service provider interface
    }
}
