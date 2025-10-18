using CampaignService_Service.DTOs;
using CampaignService_Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendRecallNotificationAsync(VehicleRecallResponseDto vehicleRecall)
        {
            _logger.LogInformation($"Sending recall notification for vehicle {vehicleRecall.VehicleId} with {vehicleRecall.ActiveCampaigns.Count} active campaigns");
            await Task.Delay(100); // Simulate notification sending
            _logger.LogInformation($"Recall notification sent for vehicle {vehicleRecall.VehicleId}");
        }

        public async Task SendAppointmentReminderAsync(CampaignAppointmentDto appointment)
        {
            _logger.LogInformation($"Sending appointment reminder for vehicle {appointment.CampaignVehicle?.VehicleId} on {appointment.AppointmentDate}");
            await Task.Delay(100); // Simulate notification sending
            _logger.LogInformation($"Appointment reminder sent for vehicle {appointment.CampaignVehicle?.VehicleId}");
        }

        public async Task SendCampaignNotificationAsync(CampaignDto campaign, IEnumerable<long> vehicleIds)
        {
            _logger.LogInformation($"Sending campaign notification for campaign {campaign.Title} to {vehicleIds.Count()} vehicles");
            await Task.Delay(100); // Simulate notification sending
            _logger.LogInformation($"Campaign notification sent for campaign {campaign.Title}");
        }
    }
}
