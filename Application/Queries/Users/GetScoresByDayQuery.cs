using Application.Models.DTOs;
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
    public class GetScoresByDayQuery : IRequest<List<ScoresResponseModel>>
    {
        public DateTime Date { get; set; }
    }

    public class GetScoresByDayQueryHandler : IRequestHandler<GetScoresByDayQuery, List<ScoresResponseModel>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetScoresByDayQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<ScoresResponseModel>> Handle(GetScoresByDayQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetScoresByDay(request.Date);
        }
    }
}
