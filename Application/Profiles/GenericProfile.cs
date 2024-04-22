using Application.Commands.Users;
using Application.Commands.UserScores;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<UserEntity, PostUserCommand>().ReverseMap();
            CreateMap<UserScoreEntity, PostUserScoreCommand>().ReverseMap();
        }
    }
}
