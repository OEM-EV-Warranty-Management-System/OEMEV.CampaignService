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
    public class CampaignRepository : ICampaignRepository
    {
        private readonly AppDbContext _context;

        public CampaignRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await _context.Campaigns
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Campaign?> GetByIdAsync(int id)
        {
            return await _context.Campaigns
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Campaign> AddAsync(Campaign campaign)
        {
            try
            {
                campaign.IsActive = true; 
                campaign.CreatedAt = DateTime.UtcNow;
                await _context.Campaigns.AddAsync(campaign);

                await _context.SaveChangesAsync();

                return campaign;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Campaign> UpdateAsync(Campaign campaign)
        {
            _context.Campaigns.Update(campaign);
            await _context.SaveChangesAsync();

            return campaign;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null || !campaign.IsActive == true)
                return false;

            campaign.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Campaign>> GetActiveCampaignsAsync()
        {
            return await _context.Campaigns
                .Where(c => c.IsActive == true && c.StartDate <= DateTime.UtcNow && c.EndDate >= DateTime.UtcNow)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Campaigns
                .Where(c => c.StartDate >= startDate && c.EndDate <= endDate)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
