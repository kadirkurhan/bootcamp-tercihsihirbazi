using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Dtos.AppUserDtos;
using TercihSihirbazi.Entities.Dtos.ProfileDtos;

namespace TercihSihirbazi.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : AutoMapper.Profile
    {
        public MapProfile()
        {
            CreateMap<ProfileAddDto, Entities.Concrete.Profile>();
            CreateMap<Entities.Concrete.Profile, ProfileAddDto>();

            CreateMap<ProfileUpdateDto, Entities.Concrete.Profile>();
            CreateMap<Entities.Concrete.Profile, ProfileUpdateDto>();

            CreateMap<AppUserAddDto, AppUser>();
            CreateMap<AppUser, AppUserAddDto>();

            CreateMap<AppUserFavorites, ProfileFavoriteDto>().ReverseMap();
        }
    }
}
