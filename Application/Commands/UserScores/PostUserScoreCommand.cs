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

namespace Application.Commands.UserScores
{
    public class PostUserScoreCommand : IRequest<RepositoryResult<UserScoreEntity>>
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Score { get; set; }
    }

    public class PostUserScoreCommandHandler : IRequestHandler<PostUserScoreCommand, RepositoryResult<UserScoreEntity>>
    {
        private readonly IUserScoreRepository _userScoreRepository;
        private readonly IUserScoreFactory _userScoreFactory;
        private readonly IRepositoryResultFactory<UserScoreEntity> _repoResultFactory;

        public PostUserScoreCommandHandler(IUserScoreFactory userScoreFactory, IRepositoryResultFactory<UserScoreEntity> repoResultFactory, IUserScoreRepository userScoreRepository)
        {
            _userScoreFactory = userScoreFactory;
            _repoResultFactory = repoResultFactory;
            _userScoreRepository = userScoreRepository;
        }


        public async Task<RepositoryResult<UserScoreEntity>> Handle(PostUserScoreCommand request, CancellationToken cancellationToken)
        {
            var entity = _userScoreFactory.Create(request);

            await _userScoreRepository.CreateAsync(entity);

            return _repoResultFactory.Create(successfull: true);
        }
    }
}
