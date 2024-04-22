using Application.Factories;
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
    public class UserScoreRepository : IUserScoreRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public UserScoreRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreateAsync(UserScoreEntity entity)
        {
            using IDbConnection connection = _connectionFactory.Create();

            await connection.ExecuteAsync("INSERT INTO UserScores(CreatedAt, UserId, Date, Score) VALUES(@CreatedAt, @UserId, @Date, @Score)",
                entity);
        }
    }
}
