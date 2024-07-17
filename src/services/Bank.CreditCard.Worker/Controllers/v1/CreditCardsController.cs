using System.Net;
using Bank.Core.Api;
using Bank.CreditCard.Worker.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bank.CreditCard.Worker.Controllers.v1;

[ApiController]
[Route("api/v1/credit-cards/{clientId:guid}")]
public class CreditCardsController : MainController
{
    private readonly ICreditCardRepository _creditCardRepository;

    public CreditCardsController(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CreditCardEntity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Get(Guid clientId)
    {
        var creditCards = await _creditCardRepository.GetByClientId(clientId);
        if(creditCards == null)
            return CustomResponse(httpStatusCode: HttpStatusCode.NoContent);

        return CustomResponse(creditCards);
    }

}