using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Controllers.Base;
using Movies.Application.Features.Commands;
using Movies.Application.Features.Queries;
using Movies.Domain.Entity;
using Movies.Domain.Shared.Enums;

namespace Movies.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseController
    {
        public ValuesController(IMediator mediator) => Mediator = mediator;

        [HttpGet("GetUsers")]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public async Task<ActionResult<ResponseResult<IEnumerable<ImdbUser>?>>> GetUsers()
            => Ok(await Mediator.Send(new GetElementListQuery<ImdbUser>(), CancellationToken.None));

        [HttpGet("GetUserById")]
        public async Task<ActionResult<ResponseResult<ImdbUser>>> GetUserById(string userName)
            => Ok(await Mediator.Send(new GetSingleElementQuery<ImdbUser>(i => i.Name == userName),
                CancellationToken.None));

        [HttpPost("AddImdbUser")]
        public async Task<ActionResult<ResponseResult<bool>>> AddImdbUser(ImdbUser imdbUser)
            => Ok(await Mediator.Send(new SaveElementCommand<ImdbUser>(imdbUser, InsertUpdateEnum.Insert),
                CancellationToken.None));
    }
}