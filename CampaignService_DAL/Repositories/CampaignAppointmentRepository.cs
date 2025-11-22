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

        public async Task<CampaignAppointment?> GetByIdAsync(long id)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .FirstOrDefaultAsync(ca => ca.Id == id);
        }

        public Task<CampaignAppointment> AddAsync(CampaignAppointment appointment)
        {
            _context.CampaignAppointments.Add(appointment);
            return Task.FromResult(appointment);
        }

        public Task<CampaignAppointment> UpdateAsync(CampaignAppointment appointment)
        {
            _context.CampaignAppointments.Update(appointment);
            return Task.FromResult(appointment);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var appointment = await _context.CampaignAppointments.FindAsync(id);
            if (appointment == null || !appointment.IsActive == true) 
                return false;

            appointment.IsActive = false;
            return true;
        }

        public async Task<IEnumerable<CampaignAppointment>> GetByCampaignVehicleIdAsync(long campaignVehicleId)
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

        public async Task<IEnumerable<CampaignAppointment>> GetByStatusAsync(string status)
        {
            return await _context.CampaignAppointments
                .Include(ca => ca.CampaignVehicle)
                .ThenInclude(cv => cv.Campaign)
                .Where(ca => ca.Status == status && ca.IsActive == true)
                .OrderByDescending(ca => ca.CreatedAt)
                .ToListAsync();
        }
    }
}
