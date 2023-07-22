using AutoMapper;
using SkillTest.Core.Application.DTOs;
using SkillTest.Core.Domain.Entity;

namespace SkillTest.Core.Application.Mappers
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
