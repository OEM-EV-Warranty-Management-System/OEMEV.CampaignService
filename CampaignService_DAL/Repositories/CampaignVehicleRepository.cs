using CampaignService_Repository.Interfaces;
using CampaignService_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Repository.Repositories
{
    public class CampaignVehicleRepository : ICampaignVehicleRepository
    {
        private readonly AppDbContext _context;

        public CampaignVehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CampaignVehicle>> GetAllAsync()
        {
            return await _context.CampaignVehicles
                .Include(cv => cv.Campaign)
                .OrderByDescending(cv => cv.CreatedAt)
                .ToListAsync();
        }

        public async Task<CampaignVehicle?> GetByIdAsync(int id)
        {
            return await _context.CampaignVehicles
                .Include(cv => cv.Campaign)
                .FirstOrDefaultAsync(cv => cv.Id == id);
        }

        public async Task<CampaignVehicle> AddAsync(CampaignVehicle campaignVehicle)
        {
            _context.CampaignVehicles.Add(campaignVehicle);
            await _context.SaveChangesAsync();
            return campaignVehicle;
        }

        public async Task<CampaignVehicle> UpdateAsync(CampaignVehicle campaignVehicle)
        {
            _context.CampaignVehicles.Update(campaignVehicle);
            await _context.SaveChangesAsync();
            return campaignVehicle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.CampaignVehicles.FindAsync(id);
            if (vehicle == null || vehicle.IsActive == true) return false;

            vehicle.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CampaignVehicle>> GetByCampaignIdAsync(int campaignId)
        {
            return await _context.CampaignVehicles
                .Include(cv => cv.Campaign)
                .Where(cv => cv.CampaignId == campaignId)
                .OrderByDescending(cv => cv.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignVehicle>> GetByVehicleIdAsync(long vehicleId)
        {
            return await _context.CampaignVehicles
                .Include(cv => cv.Campaign)
                .Where(cv => cv.VehicleId == vehicleId)
                .OrderByDescending(cv => cv.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignVehicle>> GetVehiclesWithActiveRecallsAsync()
        {
            return await _context.CampaignVehicles
                .Include(cv => cv.Campaign)
                .Where(cv => (bool)cv.Campaign.IsActive)
                .OrderByDescending(cv => cv.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> VehicleExistsInCampaignAsync(long vehicleId, int campaignId)
        {
            return await _context.CampaignVehicles
                .AnyAsync(cv => cv.VehicleId == vehicleId && cv.CampaignId == campaignId);
        }
    }
}
