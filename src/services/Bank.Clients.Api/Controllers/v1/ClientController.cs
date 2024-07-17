using System.Net;
using Bank.Clients.Api.Application.Commands;
using Bank.Clients.Api.Application.DTO;
using Bank.Clients.Api.Application.Queries;
using Bank.Core.Api;
using Bank.Core.Communication;
using Bank.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Clients.Api.Controllers.v1;

[ApiController]
[Route("api/v1/clients")]
public class ClientController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IClientQuery _clientQuery;

    public ClientController(
        IMediatorHandler mediatorHandler,
        IClientQuery clientQuery)
    {
        _mediatorHandler = mediatorHandler;
        _clientQuery = clientQuery;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ResponseResult), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Add(AddClientCommand client)
    {        
        return CustomResponse(await _mediatorHandler.SendCommand(client));
    }

    [HttpGet("document")]
    [ProducesResponseType(typeof(ClientDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Get(string document)
    {
        var client = await _clientQuery.GetByDocument(document);
        if(client == null)
            return CustomResponse(httpStatusCode: HttpStatusCode.NoContent);

        return CustomResponse(client);
    }

}