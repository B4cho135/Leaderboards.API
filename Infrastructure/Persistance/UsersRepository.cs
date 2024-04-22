using Application.Factories;
using Application.Models.ResponseModels;
using Application.Persistance;
using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public UsersRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateAsync(UserEntity user)
        {
            using IDbConnection connection = _connectionFactory.Create();

            await connection.ExecuteAsync("INSERT INTO Users(CreatedAt, FirstName, LastName, UserName) VALUES(@CreatedAt, @FirstName, @LastName, @UserName)",
                user);
        }

        public async Task<List<ScoresResponseModel>> GetAllData()
        {
            using IDbConnection connection = _connectionFactory.Create();

            var data = await connection.QueryAsync<ScoresResponseModel>(
                "select s.Id, s.UserName, us.Score " +
                "from Users s join UserScores us " +
                "on s.Id = us.UserId");

            return data.ToList();
        }

        public async Task<UserEntity?> GetByUserName(string userName)
        {
            using IDbConnection connection = _connectionFactory.Create();

            var user = await connection.QueryFirstOrDefaultAsync<UserEntity>("SELECT * FROM Users WHERE Username = @UserName", new { UserName = userName });

            return user;
        }

        public async Task<List<ScoresResponseModel>> GetScoresByDay(DateTime date)
        {
            using IDbConnection connection = _connectionFactory.Create();

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Date", date);

            var scoresByDay = await connection.QueryAsync<ScoresResponseModel>("GetScoresByDay", queryParameters, commandType: CommandType.StoredProcedure);

            return scoresByDay.ToList();
        }

        public async Task<List<ScoresResponseModel>> GetScoresByMonth(DateTime date)
        {
            using IDbConnection connection = _connectionFactory.Create();

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@Date", date);

            var scoresByDay = await connection.QueryAsync<ScoresResponseModel>("GetScoresByMonth", queryParameters, commandType: CommandType.StoredProcedure);

            return scoresByDay.ToList();
        }

        public async Task<StatsResponseModel?> GetStats()
        {
            using IDbConnection connection = _connectionFactory.Create();

            var result = await connection.QueryFirstOrDefaultAsync<StatsResponseModel>("select dbo.GetAverageDailyScore() as AverageDailyScore, dbo.GetAverageMonthlyScore() as AverageMonthlyScore, dbo.GetMaxDailyScore() as MaxDailyScore, dbo.GetMaxWeeklyScore() as MaxWeeklyScore, dbo.GetMaxMonthlyScore() as MaxMonthlyScore");

            return result;
        }

        public async Task<UserInfoResponseModel?> GetUserInfo(int userId)
        {
            using IDbConnection connection = _connectionFactory.Create();

            var queryParameters = new DynamicParameters();
            queryParameters.Add("@UserId", userId);

            var userInfo = await connection.QueryFirstOrDefaultAsync<UserInfoResponseModel>("GetUsersPlaceForCurrentMonth", queryParameters, commandType: CommandType.StoredProcedure);

            return userInfo;
        }
    }
}
