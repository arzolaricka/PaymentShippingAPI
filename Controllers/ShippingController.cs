using Microsoft.AspNetCore.Mvc;
using PaymentShippingModel;
using PaymentShippingService;

namespace PaymentShippingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly PaymentShippingService.PaymentShippingService _service;

        public ShippingController(PaymentShippingService.PaymentShippingService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Shipping>> GetAll()
        {
            return Ok(_service.ViewShipping());
        }

        [HttpPost]
        public ActionResult Add([FromBody] ShippingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Address))
                return BadRequest("Name and Address are required.");

            _service.AddShipping(request.Name, request.Address, request.Latitude, request.Longitude);
            return Ok("Shipping added successfully!");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ShippingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Address))
                return BadRequest("Name and Address are required.");

            _service.UpdateShipping(id, request.Name, request.Address, request.Latitude, request.Longitude);
            return Ok("Shipping updated successfully!");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.DeleteShipping(id);
            return Ok("Shipping deleted successfully!");
        }
    }


    public class ShippingRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}