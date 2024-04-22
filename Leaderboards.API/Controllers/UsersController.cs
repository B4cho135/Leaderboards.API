using Application.Commands.Users;
using Application.Commands.UserScores;
using Application.Queries;
using Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaderboards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("UploadUserData")]
        public async Task<IActionResult> UploadUserData(PostUserCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.Successfull)
            {
                return NoContent();
            }

            return BadRequest(result.Message);
        }

        [HttpPost("UploadUserScores")]
        public async Task<IActionResult> UploadUserScores(PostUserScoreCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Successfull)
            {
                return NoContent();
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetScoresByDay")]
        public async Task<IActionResult> GetScoresByDay(DateTime date)
        {
            var result = await _mediator.Send(new GetScoresByDayQuery() { Date = date});

            return Ok(result);
        }

        [HttpGet("GetScoresByMonth")]
        public async Task<IActionResult> GetScoresByMonth(DateTime date)
        {
            var result = await _mediator.Send(new GetScoresByMonthQuery() { Date = date });

            return Ok(result);
        }

        [HttpGet("GetAllData")]
        public async Task<IActionResult> GetAllData()
        {
            var result = await _mediator.Send(new GetAllDataQuery());

            return Ok(result);
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var result = await _mediator.Send(new GetUserInfoQuery()
            {
                UserId = userId
            });

            if(result == null)
            {
                return BadRequest("No score was found for the user");
            }

            return Ok(result);
        }

        [HttpGet("GetStats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _mediator.Send(new GetStatsQuery());

            if(result == null)
            {
                return BadRequest("Data was not found");
            }

            return Ok(result);
        }
    }
}
