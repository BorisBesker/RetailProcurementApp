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
            CreateMap<Suplier, SuplierDto>();
            CreateMap<Suplier, SuplierIdDto>();
            CreateMap<SuplierDto, Suplier>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.StoreItems, opt => opt.Ignore());
            CreateMap<SuplierStoreItem, SuplierItemDto>().
                ForMember(x => x.SuplierName, m => m.MapFrom(u => (u.Suplier != null) ? u.Suplier.Name : string.Empty)).
                ForMember(x => x.StoreItemName, m => m.MapFrom(u => (u.StoreItem != null) ? u.StoreItem.ItemName : string.Empty));
            CreateMap<SuplierItemIdDto, SuplierStoreItem>();
            CreateMap<UserLoginDto, User>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.Ignore())
                .ForMember(x => x.LastName, opt => opt.Ignore());
            CreateMap<UserRegisterDto, User>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
