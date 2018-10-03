using AutoMapper;
using Flatbuilder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatbuilder.WebAPI.Mapping
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {               
                cfg.CreateMap<Order, Flatbuilder.DTO.Order>();
                cfg.CreateMap<Flatbuilder.DTO.Order, Order >();

                cfg.CreateMap<Room, Flatbuilder.DTO.Room>();
                cfg.CreateMap<Flatbuilder.DTO.Room, Room>();

                cfg.CreateMap<Flatbuilder.DTO.Bedroom, Bedroom>().ReverseMap();
                cfg.CreateMap<Flatbuilder.DTO.Shower, Shower>().ReverseMap();
                cfg.CreateMap<Flatbuilder.DTO.Kitchen, Kitchen>().ReverseMap();

                cfg.CreateMap<Flatbuilder.DTO.Costumer, Costumer>().ReverseMap();

            });


            return config.CreateMapper();
        }
    }
}
