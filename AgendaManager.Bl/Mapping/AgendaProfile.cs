using AgendaManager.Bl.Dto;
using AgendaManager.Model.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaManager.Bl.Mapping
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile()
        {
            CreateMap<UserDto, User>()
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EventDto, Event>()
                .ReverseMap();

            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<InvitationDto, Invitation>()
               .ReverseMap();

            CreateMap<InvitationDto, Invitation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
