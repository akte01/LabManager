using AutoMapper;
using LabStore.Dtos;
using LabStore.Models;

namespace LabStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Reagent, ReagentDto>();
            Mapper.CreateMap<Equipment, EquipmentDto>();
            Mapper.CreateMap<Order, OrderDto>();

            Mapper.CreateMap<ReagentDto, Reagent>()
            .ForMember(r => r.Id, opt => opt.Ignore());
            Mapper.CreateMap<UserDto, ApplicationUser>()
            .ForMember(u => u.Id, opt => opt.Ignore());
            Mapper.CreateMap<EquipmentDto, Equipment>()
            .ForMember(e => e.Id, opt => opt.Ignore());
            Mapper.CreateMap<OrderDto, Order>()
            .ForMember(e => e.Id, opt => opt.Ignore());
        }
    }
}