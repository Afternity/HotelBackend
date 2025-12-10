using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;
using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingListVMs;

namespace HotelBackend.Domain.Interfaces.InterfacesServices
{
    public interface IBookingService
    {
        /// <summary>
        /// Поиск по индексу.
        /// GetBookingDto для единообразия. 
        /// </summary>
        Task<BookingDetailsVm?> GetByIdAsync(
            GetBookingDto getDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Создание новой записи.
        /// return Guid для подтверждения создания.
        /// </summary>
        Task<Guid> CreateAsync(
            CreateBookingDto createDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех бронирований пользователя, где IsDeleted = false.
        /// GetUserBookingDto для единообразия.
        /// </summary>
        Task<UserBookingListVm> GetAllByUserAsync(
            GetUserBookingDto findDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вывод всех бронирований, где IsDeleted = false
        /// Поддердка польного CRUD.
        /// </summary>
        Task<BookingListVm> GetAllAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Обновление бронирования.
        /// Поддердка польного CRUD.
        /// </summary>
        Task UpdateAsync(
            UpdateBookingDto updateDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Полное удаление бронирования.
        /// </summary>
        Task HardDeleteAsync(
            HardDeleteBookingDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Мягкое удаление бронирования.
        /// </summary>
        Task SoftDeleteAsync(
            HardDeleteBookingDto hardDeleteDto,
            CancellationToken cancellationToken);

        /// <summary>
        /// Получение последнего бронирования.
        /// Проверкаа происходит по дате.
        /// </summary>
        Task GetLastBookingByUserAsync(
            GetLastUserBookingDto getDto,
            CancellationToken cancellationToken);
    }
}
