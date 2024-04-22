using Application.Factories;
using Application.Models.DTOs;
using Application.Persistance;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class PostUserCommand : IRequest<RepositoryResult<UserEntity>>
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
    }

    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, RepositoryResult<UserEntity>>
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IUsersFactory _usersFactory;
        private readonly IRepositoryResultFactory<UserEntity> _repoResultFactory;

        public PostUserCommandHandler(IUsersRepository usersRepo, IUsersFactory usersFactory, IRepositoryResultFactory<UserEntity> repoResultFactory)
        {
            _usersRepo = usersRepo;
            _usersFactory = usersFactory;
            _repoResultFactory = repoResultFactory;
        }

        public async Task<RepositoryResult<UserEntity>> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepo.GetByUserName(request.UserName) != null;

            if (existingUser)
                return _repoResultFactory.Create(successfull: false, message: "User already exists with the same username!");

            var userEntity = _usersFactory.Create(request);

            await _usersRepo.CreateAsync(userEntity);

            return _repoResultFactory.Create(successfull: true);
        }
    }
}
