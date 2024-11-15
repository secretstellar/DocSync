namespace DocSync.API.Mappings
{
    using AutoMapper;
    using DocSync.API.DTOs;
    using DocSync.API.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentInfo, DocumentInfoDto>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Status));

            CreateMap<DocumentInfoDto, DocumentInfo>()
              .ForPath(dest => dest.Status.Id, opt => opt.MapFrom(src => src.StatusId));

            CreateMap<UserDetails, UserDetailsDto>()
                .ReverseMap();

            CreateMap<DocumentModel, DocumentDto>()
                .ReverseMap();

            CreateMap<PeopleCsv, PeopleCsvDto>()
                .ReverseMap();
        }
    }

}
