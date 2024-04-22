using Application.Commands.Users;
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
    public class UsersFactory : IUsersFactory
    {
        private readonly IMapper _mapper;

        public UsersFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserEntity Create(PostUserCommand command)
        {
            return _mapper.Map<UserEntity>(command);
        }
    }
}
