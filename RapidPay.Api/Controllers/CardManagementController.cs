using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Services.CreditCards;
using RapidPay.Services.CreditCards.Models;
using RapidPay.Services.Payments;
using RapidPay.Services.Payments.Models;
using System.Net;

namespace RapidPay.Api.Controllers
{
    /// <summary>
    /// Card Management controller with authentication.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardManagementController : ControllerBase
    {
        private readonly ICreditCardsService _creditCardsService;

        private readonly IPaymentsService _paymentsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardManagementController"/> class.
        /// Card Management controller constructor.
        /// </summary>
        /// <param name="creditCardsService">credit cards service.</param>
        /// <param name="paymentsService">payments service.</param>
        public CardManagementController(ICreditCardsService creditCardsService, IPaymentsService paymentsService)
        {
            _creditCardsService = creditCardsService;
            _paymentsService = paymentsService;
        }

        /// <summary>Create a new credit card.</summary>
        /// <param name="creditCardInput">Credit Card data input.</param>
        /// <response code ="200">Returns credit card.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Credit Card.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreditCardResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("create-card")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCreditCard([FromBody] CreditCardInput creditCardInput)
        {
            return Ok(await _creditCardsService.AddCreditCardAsync(creditCardInput));
        }

        /// <summary>Get card balance.</summary>
        /// <param name="cardBalanceInput">Get card balance data input.</param>
        /// <response code ="200">Returns card balance.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Credit Card.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(CardBalanceInput), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("get-card-balance")]
        public async Task<IActionResult> GetCardBalance([FromQuery] CardBalanceInput cardBalanceInput)
        {
            return Ok(await _creditCardsService.GetCardBalanceAsync(cardBalanceInput));
        }

        /// <summary>Create a new payment.</summary>
        /// <param name="paymentInput">Payment data input.</param>
        /// <response code ="200">Returns payment.</response>
        /// <response code ="401">Returns an Unauthorized Error.</response>
        /// <response code ="403">Returns a Forbidden Error.</response>
        /// <response code ="500">Returns a server error.</response>
        /// <remarks>Payment.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaymentResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("pay")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentInput paymentInput)
        {
            return Ok(await _paymentsService.AddPaymentAsync(paymentInput));
        }
    }
}
