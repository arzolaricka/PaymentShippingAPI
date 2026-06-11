using Microsoft.AspNetCore.Mvc;
using PaymentShippingDataService;
using PaymentShippingModel;
using PaymentShippingService;

namespace PaymentShippingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentShippingService.PaymentShippingService _service;

        public PaymentController(PaymentShippingService.PaymentShippingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Payment>> GetAll()
        {
            return Ok(_service.ViewPayments());
        }

        [HttpPost]
        public ActionResult Add([FromBody] Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.Method) || string.IsNullOrWhiteSpace(payment.AccountName))
                return BadRequest("Method and AccountName are required.");

            _service.AddPayment(payment.Method, payment.AccountName, payment.AccountNumber);
            return Ok("Payment added successfully!");
        }

        [HttpPost("creditcard")]
        public ActionResult AddCreditCard([FromBody] CreditCardRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NameOnCard) ||
                string.IsNullOrWhiteSpace(request.CardNumber) ||
                string.IsNullOrWhiteSpace(request.Expiry) ||
                string.IsNullOrWhiteSpace(request.CVV))
                return BadRequest("All credit card fields are required.");

            if (request.CardNumber.Length != 16)
                return BadRequest("Card number must be exactly 16 digits.");

            _service.AddCreditCardPayment(request.NameOnCard, request.CardNumber, request.Expiry, request.CVV);
            return Ok("Credit Card added successfully!");
        }

        // POST: api/Payment/bankaccount
        [HttpPost("bankaccount")]
        public ActionResult AddBankAccount([FromBody] BankAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.BankName) ||
                string.IsNullOrWhiteSpace(request.AccountHolder) ||
                string.IsNullOrWhiteSpace(request.AccountNumber))
                return BadRequest("All bank account fields are required.");

            _service.AddBankAccountPayment(request.BankName, request.AccountHolder, request.AccountNumber);
            return Ok("Bank Account added successfully!");
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.Method) || string.IsNullOrWhiteSpace(payment.AccountName))
                return BadRequest("Method and AccountName are required.");

            _service.UpdatePayment(id, payment.Method, payment.AccountName, payment.AccountNumber);
            return Ok("Payment updated successfully!");
        }

        // PUT: api/Payment/creditcard/5
        [HttpPut("creditcard/{id}")]
        public ActionResult UpdateCreditCard(int id, [FromBody] CreditCardRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NameOnCard) ||
                string.IsNullOrWhiteSpace(request.CardNumber) ||
                string.IsNullOrWhiteSpace(request.Expiry) ||
                string.IsNullOrWhiteSpace(request.CVV))
                return BadRequest("All credit card fields are required.");

            if (request.CardNumber.Length != 16)
                return BadRequest("Card number must be exactly 16 digits.");

            _service.UpdateCreditCardPayment(id, request.NameOnCard, request.CardNumber, request.Expiry, request.CVV);
            return Ok("Credit Card updated successfully!");
        }

        // PUT: api/Payment/bankaccount/5
        [HttpPut("bankaccount/{id}")]
        public ActionResult UpdateBankAccount(int id, [FromBody] BankAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.BankName) ||
                string.IsNullOrWhiteSpace(request.AccountHolder) ||
                string.IsNullOrWhiteSpace(request.AccountNumber))
                return BadRequest("All bank account fields are required.");

            _service.UpdateBankAccountPayment(id, request.BankName, request.AccountHolder, request.AccountNumber);
            return Ok("Bank Account updated successfully!");
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeletePayment(id);
            return Ok("Payment deleted successfully!");
        }
    }

    // ─── Request Models ──────────────────────────────────────────────────────

    public class CreditCardRequest
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string CVV { get; set; }
    }

    public class BankAccountRequest
    {
        public string BankName { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
    }
}
