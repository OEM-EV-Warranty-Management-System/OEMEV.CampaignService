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
    public class CampaignAppointmentRepository : ICampaignAppointmentRepository
    {
        private readonly AppDbContext _context;

        public CampaignAppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CampaignAppointment>> GetAllAsync()
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .OrderByDescending(ca => ca.CreatedAt)
                .ToListAsync();
        }

        public async Task<CampaignAppointment?> GetByIdAsync(int id)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .FirstOrDefaultAsync(ca => ca.Id == id);
        }

        public async Task<CampaignAppointment> AddAsync(CampaignAppointment appointment)
        {
            _context.CampaignAppointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<CampaignAppointment> UpdateAsync(CampaignAppointment appointment)
        {
            _context.CampaignAppointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.CampaignAppointments.FindAsync(id);
            if (appointment == null) return false;

            _context.CampaignAppointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CampaignAppointment>> GetByCampaignVehicleIdAsync(int campaignVehicleId)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.CampaignVehicleId == campaignVehicleId)
                .OrderByDescending(ca => ca.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignAppointment>> GetByVehicleIdAsync(long vehicleId)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.CampaignVehicle.VehicleId == vehicleId)
                .OrderByDescending(ca => ca.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignAppointment>> GetByServiceCenterIdAsync(long serviceCenterId)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.ServiceCenterId == serviceCenterId)
                .OrderByDescending(ca => ca.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignAppointment>> GetByTechnicianIdAsync(Guid technicianId)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.TechnicianId == technicianId)
                .OrderByDescending(ca => ca.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignAppointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.AppointmentDate >= startDate && ca.AppointmentDate <= endDate)
                .OrderBy(ca => ca.AppointmentDate)
                .ToListAsync();
        }
    }
}
