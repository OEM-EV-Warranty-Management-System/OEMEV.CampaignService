using AutoMapper;
using CampaignService_Repository.Models;
using CampaignService_Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignService_Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Campaign, CampaignDto>();
            CreateMap<CreateCampaignDto, Campaign>();

            CreateMap<CampaignVehicle, CampaignVehicleDto>();
            CreateMap<CreateCampaignVehicleDto, CampaignVehicle>();

            CreateMap<CampaignAppointment, CampaignAppointmentDto>();
            CreateMap<CreateAppointmentDto, CampaignAppointment>();
        }
    }
}
