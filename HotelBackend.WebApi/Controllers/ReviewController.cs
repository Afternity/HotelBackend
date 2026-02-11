using HotelBackend.Domain.Interfaces.InterfacesServices;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewListVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController
        : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(
            IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("get-by-id")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewDetailsVm>> Get(
            [FromQuery] GetReviewDto getDto,
            CancellationToken cancellationToken)
        {
            var review = await _reviewService
                .GetByIdAsync(getDto, cancellationToken);

            return review == null ? NotFound() : Ok(review);
        }

        [HttpPost("create")]
        [Authorize(Policy = PolicyConsts.ClientOnly)]
        public async Task<IActionResult> Create(
            [FromBody] CreateReviewDto createDto,
            CancellationToken cancellationToken)
        {
            var reviewId = await _reviewService
                .CreateAsync(createDto, cancellationToken);

            if (reviewId == Guid.Empty)
                return BadRequest("Не удалось создать отзыв");

            return Ok(reviewId);
        }

        [HttpPut("update")]
        [Authorize(Policy = PolicyConsts.ClientOnly)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateReviewDto updateDto,
            CancellationToken cancellationToken)
        {
            await _reviewService
                .UpdateAsync(updateDto, cancellationToken);

            return NoContent();
        }

        [HttpDelete("hard-delete")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> Delete(
            [FromQuery] HardDeleteReviewDto hardDeleteDto,
            CancellationToken cancellationToken)
        {
            await _reviewService
                .HardDeleteAsync(hardDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpPatch("soft-delete")]
        [Authorize(Policy = PolicyConsts.AdminOnly)]
        public async Task<IActionResult> SoftDelete(
            [FromBody] SoftDeleteReviewDto softDeleteDto,
            CancellationToken cancellationToken)
        {
            await _reviewService
                .SoftDeleteAsync(softDeleteDto, cancellationToken);

            return NoContent();
        }

        [HttpGet("get-all")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewListVm>> GetAll(
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService
                .GetAllAsync(cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("get-by-rating-and-room")]
        [AllowAnonymous]
        public async Task<ActionResult<RatingAndRoomReviewListVm>> GetByRatingAndRoom(
            [FromQuery] GetAllByRatingAndRoomReviewDto getAllDto,
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService
                .GetAllByRatingAndRoomAsync(getAllDto, cancellationToken);

            return Ok(reviews);
        }
    }
}
