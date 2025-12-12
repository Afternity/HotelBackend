using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.CreatePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.DeletePaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.GetPaymentDTOs;
using HotelBackend.Shared.Contracts.DTOs.PaymentDTOs.UpdatePaymentDTOs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.PaymentVMs.PaymentListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IPaymentService
    {
        /// <summary>
        /// Поиск платежа по Id.
        /// GetPaymentDto для единообразия.
        /// </summary>
        Task<PaymentDetailsVm?> GetByIdAsync(
            GetPaymentDto getDto, 
            CancellationToken cancellationToken);

        /// <summary>
        /// Создание новой записи Payment.
        /// </summary>
        Task<Guid> CreateAsync(
            CreatePaymentDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Обновление записи Payment.
        /// </summary>
        Task UpdateAsync(
            UpdatePaymentDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление Payment.
        /// HardDeletePaymentDto для единообразия.
        /// </summary>
        Task HardDeleteAsync(
            HardDeletePaymentDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление Payment.
        /// SoftDeletePaymentDto для единообразия.
        /// </summary>
        Task SoftDeleteAsync(
            SoftDeletePaymentDto softDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех бронирований, где IsDeleted = false
        /// </summary>
        Task<PaymentListVm> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех Payment пользователя, где IsDeleted = false.
        /// </summary>
        Task<UserPaymentListVm> GetAllByUserAsync(
            GetAllByUserPaymentDto getAllDto,
            CancellationToken cancellationToken);
    }
}
