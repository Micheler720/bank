using System.Net;
using Bank.Core.Communication;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Clients.Api.Controllers.v1;

[ApiController]
[Route("api/v1/clients")]
public class ClientController : MainController
{
    [HttpPost()]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResponseResult), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register(object teste)
    {
        return Ok();
    }

}