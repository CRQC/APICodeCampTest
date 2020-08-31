using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data
{
    public class CampMappingProfile : Profile
    {

        public CampMappingProfile()
        {
            //CreateMap<Camp, CampModel>()
            //    .ForMember(x => x.Venue, opt => opt.MapFrom(m => m.Location.VenueName))
            //    .ReverseMap();
            
            CreateMap<Camp, CampModel>().ReverseMap();

            CreateMap<Talk, TalkModel>();
            CreateMap<Speaker, SpeakerModel>();
        }

    }
}