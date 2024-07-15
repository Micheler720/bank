using System.Net;
using Bank.Clients.Api.Application.Commands;
using Bank.Core.Communication;
using Bank.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Clients.Api.Controllers.v1;

[ApiController]
[Route("api/v1/clients")]
public class ClientController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public ClientController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResponseResult), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register(AddClientCommand client)
    {        
        return CustomResponse(await _mediatorHandler.SendCommand(client));
    }

}