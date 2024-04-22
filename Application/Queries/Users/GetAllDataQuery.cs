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
    public class GetAllDataQuery : IRequest<List<ScoresResponseModel>>
    {
    }

    public class GetAllDataQueryHandler : IRequestHandler<GetAllDataQuery, List<ScoresResponseModel>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetAllDataQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<ScoresResponseModel>> Handle(GetAllDataQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetAllData();
        }
    }
}
