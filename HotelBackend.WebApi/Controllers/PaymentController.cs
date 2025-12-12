using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.DeletePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.GetPaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.UpdatePaymentDTOs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentListVMs;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController
        : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<PaymentDetailsVm>> Get(
            [FromQuery] GetPaymentDto getDto,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService
                .GetByIdAsync(getDto, cancellationToken);

            return payment == null ? NotFound() : Ok(payment);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreatePaymentDto createDto,
            CancellationToken cancellationToken)
        {
            var paymentId = await _paymentService
                .CreateAsync(createDto, cancellationToken);

            if (paymentId == Guid.Empty)
                return BadRequest("Не удалось создать платеж");

            return Ok(paymentId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdatePaymentDto updateDto,
            CancellationToken cancellationToken)
        {
            await _paymentService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeletePaymentDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _paymentService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeletePaymentDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _paymentService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<PaymentListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var payments = await _paymentService
                .GetAllAsync(cancellationToken);

            return Ok(payments);
        }

        [HttpGet("get-user-payments")]
        public async Task<ActionResult<UserPaymentListVm>> GetAllByUser(
            [FromQuery] GetAllByUserPaymentDto getAllDto,
            CancellationToken cancellationToken)
        {
            var payments = await _paymentService
                .GetAllByUserAsync(getAllDto, cancellationToken);

            return Ok(payments);
        }
    }
}
