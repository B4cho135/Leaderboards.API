using Application.Models.ResponseModels;
using Application.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetStatsQuery : IRequest<StatsResponseModel?>
    {
    }

    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, StatsResponseModel?>
    {
        private readonly IUsersRepository _usersRepository;

        public GetStatsQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<StatsResponseModel?> Handle(GetStatsQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetStats();
        }
    }
}
