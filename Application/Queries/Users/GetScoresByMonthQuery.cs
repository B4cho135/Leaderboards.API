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
    public class GetScoresByMonthQuery : IRequest<List<ScoresResponseModel>>
    {
        public DateTime Date { get; set; }
    }

    public class GetScoresByMonthQueryHandler : IRequestHandler<GetScoresByMonthQuery, List<ScoresResponseModel>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetScoresByMonthQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<ScoresResponseModel>> Handle(GetScoresByMonthQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetScoresByMonth(request.Date);
        }
    }
}
