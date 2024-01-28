using AutoMapper;
using Infrastructure.Models;
using RetailProcurementApp.Dto;

namespace RetailProcurementApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<StoreItem, StoreItemDto>();
            CreateMap<StoreItem, StoreItemIdDto>();
            CreateMap<StoreItemDto, StoreItem>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Supliers, opt => opt.Ignore());
        }
    }
}
