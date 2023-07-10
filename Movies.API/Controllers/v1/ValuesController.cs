using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Controllers.Base;
using Movies.Application.Features.Queries;
using Movies.Domain.Entity;
using Movies.Persistence;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}