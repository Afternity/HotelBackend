using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.DeleteReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.GetReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IReviewService
    {
        /// <summary>
        /// Поиск Review по Id.
        /// GetReviewDto для единообразия.
        /// </summary>
        Task<ReviewDetailsVm?> GetByIdAsync(
            GetReviewDto getDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создание нового Review.
        /// </summary>
        Task<Guid> CreateAsync(
            CreateReviewDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное обновление Review.
        /// </summary>
        Task UpdateAsync(
            UpdateReviewDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление.
        /// HardDeleteReviewDto для единообразия.
        /// </summary>
        Task HardDeleteAsync(
            HardDeleteReviewDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление.
        /// SoftDeleteReviewDto для единообразия.
        /// </summary>
        Task SoftDeleteAsync(
            SoftDeleteReviewDto softDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех Review, где где IsDeleted = false
        /// </summary>
        Task<ReviewListVm> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех Review определённой Room и определенного Rating, где IsDeleted = false
        /// </summary>
        Task<RatingAndRoomReviewListVm> GetAllByRatingAndRoomAsync(
            GetAllByRatingAndRoomReviewDto getAllDto,
            CancellationToken cancellationToken);

    }
}
