using AutoMapper;
using Dominos.Api.DTO;
using Dominos.Core.Models;

namespace Dominos.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coordinate, CoordinateDTO>();
            CreateMap<CoordinateDTO, Coordinate>();
        }
    }
}
