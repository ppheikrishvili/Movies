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
        public async Task<ImdbUser?> GetUserById(string userName)
        {
            return (await _mediator?.Send(new GetElementListQuery<ImdbUser>(i => i.Name == userName),
                CancellationToken.None)!).FirstOrDefault();
            //var t = await _mediator?.Send(
            //    new GetMovieFromSourceQuery<Movie>("", "IMDBBaseClient", "API/SearchMovie/pk_0acwtqe291brqbd3d"),
            //    CancellationToken.None)!;

            //return "value";
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] Movie val)
        //{
        //    int j = 8;
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //    int j = 8;
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}