using Application.Commands.UserScores;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IUserScoreFactory
    {
        UserScoreEntity Create(PostUserScoreCommand command);
    }
}
