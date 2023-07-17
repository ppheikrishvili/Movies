using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Controllers.Base;
using Movies.Application.Features.Commands;
using Movies.Application.Features.Queries;
using Movies.Domain.Entity;
using Movies.Domain.Shared.Enums;
using Movies.Persistence;
using Movies.Persistence.Data.Map;
using Movies.Persistence.Data.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<ValuesController>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public async Task<IEnumerable<Actor>?> Get()
        {
            //. GetElementListHandler<Actor>(new Base<Actor>()
            //var t = await _mediator.Send(new GetElementList<Actor>.GetElementListQuery(), CancellationToken.None);
            var t = await _mediator?.Send(new GetElementListQuery<Actor>(b => b.id != null), CancellationToken.None)!;

            return null;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            //var t = await _mediator?.Send(
            //    new GetMovieFromSourceQuery<Movie>("", "IMDBBaseClient", "API/SearchMovie/k_5ldd8aec"),
            //    CancellationToken.None)!;

            //var t = await _mediator?.Send(
            //    new GetMovieFromSourceQuery<Actor>(),
            //    CancellationToken.None)!;

            var t = await _mediator?.Send(
                new GetMovieFromSourceQuery<Movie>("", "IMDBBaseClient", "API/SearchMovie/pk_0acwtqe291brqbd3d"),
                CancellationToken.None)!;

            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Movie val)
        {
            int j = 8;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            int j = 8;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}