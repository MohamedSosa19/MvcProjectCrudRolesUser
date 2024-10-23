using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.ProfileMap
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
                CreateMap<RoleViewModel,IdentityRole>()
                .ForMember(d=>d.Name,o=>o.MapFrom(S=>S.RoleName));
        }
    }
}
