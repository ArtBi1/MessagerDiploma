using AutoMapper;
using UserService.DTO;
using UserService.Model;

namespace UserService.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RoleType, RoleId>()
                .ConvertUsing(src => ConvertRoleTypeToRoleId(src));

            CreateMap<RoleId, RoleType>()
                .ConvertUsing(src => ConvertRoleIdToRoleType(src));
        }

        private RoleId ConvertRoleTypeToRoleId(RoleType roleType)
        {
            switch (roleType)
            {
                case RoleType.Admin:
                    return RoleId.Admin;
                case RoleType.User:
                    return RoleId.User;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null);
            }
        }

        private RoleType ConvertRoleIdToRoleType(RoleId roleId)
        {
            switch (roleId)
            {
                case RoleId.Admin:
                    return RoleType.Admin;
                case RoleId.User:
                    return RoleType.User;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleId), roleId, null);
            }
        }

    }
}