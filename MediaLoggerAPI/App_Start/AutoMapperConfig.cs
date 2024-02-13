using AutoMapper;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.Entities.Business;

namespace MediaLoggerAPI.App_Start
{
    internal static class AutoMapperConfig
    {
        /// <summary>
        /// Add Auto Mapper Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            return services.AddSingleton(mapper);
        }
    }
    /// <summary>
    /// Mapping Profile configuration
    /// </summary>
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Es necesario crear mapas de los atributos de clases que su nombre sea de más de una palabra
            #region Security
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.IdTypeDocument, opt => opt.MapFrom(src => src.ID_TYPE_DOCUMENT))
                .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.ID_ROLE))
                .ForMember(dest => dest.IdUserCreated, opt => opt.MapFrom(src => src.ID_USER_CREATED))
                .ForMember(dest => dest.IdUserUpdated, opt => opt.MapFrom(src => src.ID_USER_UPDATED))
                .ForMember(dest => dest.ProfileImg, opt => opt.MapFrom(src => src.PROFILE_IMG))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DATE_CREATED))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DATE_UPDATED))
                .ForMember(dest => dest.TypeDocument, opt => opt.MapFrom(src => src.TYPE_DOCUMENT))
                .ForMember(dest => dest.UserCreated, opt => opt.MapFrom(src => src.USER_CREATED_NAME))
                .ForMember(dest => dest.UserUpdated, opt => opt.MapFrom(src => src.USER_UPDATED_NAME));





            #endregion

            #region Business
            CreateMap<PayPad, PayPadDto>()
                .ForMember(dest => dest.IdCurrency, opt => opt.MapFrom(src => src.ID_CURRENCY))
                .ForMember(dest => dest.IdOffice, opt => opt.MapFrom(src => src.ID_OFFICE))
                .ForMember(dest => dest.IdUserCreated, opt => opt.MapFrom(src => src.ID_USER_CREATED))
                .ForMember(dest => dest.IdUserUpdated, opt => opt.MapFrom(src => src.ID_USER_UPDATED))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DATE_CREATED))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DATE_UPDATED))
                .ForMember(dest => dest.UserCreated, opt => opt.MapFrom(src => src.USER_CREATED_NAME))
                .ForMember(dest => dest.UserUpdated, opt => opt.MapFrom(src => src.USER_UPDATED_NAME));

            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.IdUserLinked, opt => opt.MapFrom(src => src.ID_USER_LINKED))
                .ForMember(dest => dest.UserLinked, opt => opt.MapFrom(src => src.USER_LINKED))
                .ForMember(dest => dest.IdRegion, opt => opt.MapFrom(src => src.ID_REGION))
                .ForMember(dest => dest.IdUserCreated, opt => opt.MapFrom(src => src.ID_USER_CREATED))
                .ForMember(dest => dest.IdUserUpdated, opt => opt.MapFrom(src => src.ID_USER_UPDATED))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DATE_CREATED))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DATE_UPDATED))
                .ForMember(dest => dest.UserCreated, opt => opt.MapFrom(src => src.USER_CREATED_NAME))
                .ForMember(dest => dest.UserUpdated, opt => opt.MapFrom(src => src.USER_UPDATED_NAME));

            #endregion



        }
    }
}
