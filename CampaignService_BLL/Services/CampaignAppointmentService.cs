using AutoMapper;
using CampaignService_Repository.Interfaces;
using CampaignService_Repository.Models;
using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.Services
{
    public class CampaignAppointmentService : ICampaignAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public CampaignAppointmentService(
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampaignAppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetAllAsync();
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }

        public async Task<CampaignAppointmentDto?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _unitOfWork.CampaignAppointments.GetByIdAsync(id);
            return _mapper.Map<CampaignAppointmentDto>(appointment);
        }

        public async Task<CampaignAppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = _mapper.Map<CampaignAppointment>(createAppointmentDto);
            appointment.CreatedAt = DateTime.UtcNow;

            var createdAppointment = await _unitOfWork.CampaignAppointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            var appointmentDto = _mapper.Map<CampaignAppointmentDto>(createdAppointment);
            await _notificationService.SendAppointmentReminderAsync(appointmentDto);

            return appointmentDto;
        }

        public async Task<CampaignAppointmentDto?> UpdateAppointmentAsync(int id, CreateAppointmentDto updateAppointmentDto)
        {
            var existingAppointment = await _unitOfWork.CampaignAppointments.GetByIdAsync(id);
            if (existingAppointment == null) return null;

            _mapper.Map(updateAppointmentDto, existingAppointment);
            var updatedAppointment = await _unitOfWork.CampaignAppointments.UpdateAsync(existingAppointment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CampaignAppointmentDto>(updatedAppointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var result = await _unitOfWork.CampaignAppointments.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByCampaignVehicleIdAsync(int campaignVehicleId)
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetByCampaignVehicleIdAsync(campaignVehicleId);
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByVehicleIdAsync(long vehicleId)
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetByVehicleIdAsync(vehicleId);
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }
        public async Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByServiceCenterIdAsync(long serviceCenterId)
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetByServiceCenterIdAsync(serviceCenterId);
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByTechnicianIdAsync(Guid technicianId)
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetByTechnicianIdAsync(technicianId);
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<CampaignAppointmentDto>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var appointments = await _unitOfWork.CampaignAppointments.GetAppointmentsByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<CampaignAppointmentDto>>(appointments);
        }
    }
}
