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
    public class GetUserInfoQuery : IRequest<UserInfoResponseModel?>
    {
        public int UserId { get; set; }
    }

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoResponseModel?>
    {
        private readonly IUsersRepository _usersRepository;

        public GetUserInfoQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserInfoResponseModel?> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetUserInfo(request.UserId);
        }
    }
}
