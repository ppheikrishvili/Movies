using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers.Base;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    public IMediator Mediator
    {
        get => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
        set => _mediator = value;
    }
}