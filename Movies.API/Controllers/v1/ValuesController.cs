using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Controllers.Base;
using Movies.Application.Features.Queries;
using Movies.Domain.Entity;
using Movies.Persistence;

namespace Movies.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ValuesController : BaseController
    {
        private AppDBContext appDBContext;

        public ValuesController(AppDBContext _appDBContext, IMediator mediator)
        {
            appDBContext = _appDBContext;
            _mediator = mediator;
        }

        [HttpGet("GetUsers")]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public async Task<IEnumerable<ImdbUser>?> GetUsers() =>
            await _mediator?.Send(new GetElementListQuery<ImdbUser>(), CancellationToken.None)!;


        [HttpGet("GetUserById")]
        [ResponseCache(CacheProfileName = "Cache3Mins")]
        public async Task<ResponseResult<ImdbUser>> GetUserById(string userName)
        {
            return await _mediator?.Send(new GetSingleElementQuery<ImdbUser>(i => i.Name == userName),
                CancellationToken.None)!;
        }
    }
}