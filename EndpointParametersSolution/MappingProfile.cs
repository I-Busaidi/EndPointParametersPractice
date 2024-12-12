using AutoMapper;
using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Models;

namespace EndpointParametersSolution
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductInputDTO>();
            CreateMap<Product, ProductOutputDTO>();
        }
    }
}
