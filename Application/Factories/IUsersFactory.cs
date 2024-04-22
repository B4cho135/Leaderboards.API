using Application.Commands.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IUsersFactory
    {
        UserEntity Create(PostUserCommand command);
    }
}
