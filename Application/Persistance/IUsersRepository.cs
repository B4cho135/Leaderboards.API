using Application.Models.DTOs;
using Application.Models.ResponseModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance
{
    public interface IUsersRepository
    {
        Task CreateAsync(UserEntity user);
        Task<UserEntity?> GetByUserName(string userName);
        Task<List<ScoresResponseModel>> GetScoresByDay(DateTime date);
        Task<List<ScoresResponseModel>> GetScoresByMonth(DateTime date);
        Task<List<ScoresResponseModel>> GetAllData();
        Task<UserInfoResponseModel?> GetUserInfo(int userId);
        Task<StatsResponseModel?> GetStats();
    }
}
