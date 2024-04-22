using Application.Commands.UserScores;
using Application.Factories;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public class UserScoreFactory : IUserScoreFactory
    {
        private readonly IMapper _mapper;

        public UserScoreFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserScoreEntity Create(PostUserScoreCommand command)
        {
            return _mapper.Map<UserScoreEntity>(command);
        }
    }
}
