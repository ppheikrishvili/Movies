using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers.Base;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    internal IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}