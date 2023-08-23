using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Controllers.Base;
using Movies.Application.Features.Queries;
using Movies.Domain.Entity;

namespace Movies.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseController
    {
        public ValuesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("GetUsers")]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public async Task<ActionResult<ResponseResult<IEnumerable<ImdbUser>?>>> GetUsers()
            => Ok(await _mediator?.Send(new GetElementListQuery<ImdbUser>(), CancellationToken.None)!);

        [HttpGet("GetUserById")]
        [ResponseCache(CacheProfileName = "Cache3Mins")]
        public async Task<ActionResult<ResponseResult<ImdbUser>>> GetUserById(string userName)
            => Ok(await _mediator?.Send(new GetSingleElementQuery<ImdbUser>(i => i.Name == userName),
                CancellationToken.None)!);

        [HttpPost("AddImdbUser")]
        public ActionResult<ResponseResult<string>> AddImdbUser(ImdbUser imdbUser)
        {
            //return Ok(await _mediator?.Send(new GetSingleElementQuery<ImdbUser>(i => i.Name == userName),
            //    CancellationToken.None)!);
            return Ok(new ResponseResult<string>(
                @"await _mediator?.Send(new GetSingleElementQuery<ImdbUser>(i => i.Name == userName),
               CancellationToken.None)!"));
        }
    }
}