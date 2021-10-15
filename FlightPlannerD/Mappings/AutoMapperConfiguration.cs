using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequest, Flight>()
                    .ForMember(f => f.Id, opt => opt.Ignore());
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(a => a.AirportCode, opt => opt.MapFrom(s => s.Airport))
                    .ForMember(a => a.Id, opt => opt.Ignore());
                cfg.CreateMap<Flight, FlightResponse>();

                cfg.CreateMap<Airport, AirportResponse>()
                    .ForMember(a => a.Airport, opt => opt.MapFrom(s => s.AirportCode));

            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            return mapper;
        }
    }
}
