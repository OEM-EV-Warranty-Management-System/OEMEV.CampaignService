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
            CreateMap<Campaign, CampaignDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Campaign, CampaignSummaryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateCampaignDto, Campaign>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CampaignVehicles, opt => opt.Ignore());

            CreateMap<CampaignVehicle, CampaignVehicleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Campaign, opt => opt.MapFrom(src => src.Campaign));

            CreateMap<CampaignVehicle, CampaignVehicleSummaryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Campaign, opt => opt.MapFrom(src => src.Campaign));

            CreateMap<CreateCampaignVehicleDto, CampaignVehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Campaign, opt => opt.Ignore())
                .ForMember(dest => dest.CampaignAppointments, opt => opt.Ignore());

            CreateMap<CampaignAppointment, CampaignAppointmentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CampaignVehicle, opt => opt.MapFrom(src => src.CampaignVehicle));

            CreateMap<CreateAppointmentDto, CampaignAppointment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CampaignVehicle, opt => opt.Ignore());
        }
    }
}
