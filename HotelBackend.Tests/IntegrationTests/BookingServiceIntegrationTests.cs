//using AutoMapper;
//using FluentValidation;
//using FluentValidation.Results;
//using HotelBackend.Application.Features.Services;
//using HotelBackend.Domain.Enums.PaymentEnums;
//using HotelBackend.Domain.Enums.RoomEnums;
//using HotelBackend.Domain.Interfaces.InterfacesRepositories;
//using HotelBackend.Domain.Models;
//using HotelBackend.Persistence.Data.DbContexts;
//using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.CreateBookingDTOs;
//using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.DeleteBookingDTOs;
//using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.GetBookingDTOs;
//using HotelBackend.Shared.Contracts.DTOs.BookingDTOs.UpdateBookingDTOs;
//using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingDetailsVMs;
//using HotelBackend.Shared.Contracts.VMs.BookingVMs.BookingLookupDTOs;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace HotelBackend.Tests.IntegrationTests
//{
//    public class BookingServiceIntegrationTests
//        : IDisposable
//    {
//        private readonly HotelBackendDbContext _dbContext;
//        private readonly Mock<IBookingRepository> _mockBookingRepository;
//        private readonly Mock<IRoomRepository> _mockRoomRepository;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly Mock<ILogger<BookingService>> _mockLogger;
//        private readonly Mock<IValidator<CreateBookingDto>> _mockCreateValidator;
//        private readonly Mock<IValidator<UpdateBookingDto>> _mockUpdateValidator;
//        private readonly Mock<IValidator<HardDeleteBookingDto>> _mockHardDeleteValidator;
//        private readonly Mock<IValidator<SoftDeleteBookingDto>> _mockSoftDeleteValidator;
//        private readonly Mock<IValidator<GetBookingDto>> _mockGetBookingValidator;
//        private readonly Mock<IValidator<GetLastUserBookingDto>> _mockGetLastUserBookingValidator;
//        private readonly Mock<IValidator<GetUserBookingsDto>> _mockGetUserBookingsValidator;

//        private readonly BookingService _bookingService;
//        private readonly CancellationToken _cancellationToken = CancellationToken.None;

//        public BookingServiceIntegrationTests()
//        {
//            // 1. Создаем InMemory базу данных
//            var options = new DbContextOptionsBuilder<HotelBackendDbContext>()
//                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
//                .Options;

//            _dbContext = new HotelBackendDbContext(options);

//            // 2. Инициализируем все моки
//            _mockBookingRepository = new Mock<IBookingRepository>();
//            _mockRoomRepository = new Mock<IRoomRepository>();
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<BookingService>>();

//            _mockCreateValidator = new Mock<IValidator<CreateBookingDto>>();
//            _mockUpdateValidator = new Mock<IValidator<UpdateBookingDto>>();
//            _mockHardDeleteValidator = new Mock<IValidator<HardDeleteBookingDto>>();
//            _mockSoftDeleteValidator = new Mock<IValidator<SoftDeleteBookingDto>>();
//            _mockGetBookingValidator = new Mock<IValidator<GetBookingDto>>();
//            _mockGetLastUserBookingValidator = new Mock<IValidator<GetLastUserBookingDto>>();
//            _mockGetUserBookingsValidator = new Mock<IValidator<GetUserBookingsDto>>();

//            // 3. Настраиваем валидаторы (всегда возвращают успех для тестов)
//            SetupValidators();

//            // 4. Настраиваем репозитории для работы с реальной InMemory БД
//            SetupMockRepositories();

//            // 5. Создаем сервис
//            _bookingService = new BookingService(
//                _mockBookingRepository.Object,
//                _mockRoomRepository.Object,
//                _mockMapper.Object,
//                _mockLogger.Object,
//                _mockCreateValidator.Object,
//                _mockUpdateValidator.Object,
//                _mockHardDeleteValidator.Object,
//                _mockSoftDeleteValidator.Object,
//                _mockGetBookingValidator.Object,
//                _mockGetLastUserBookingValidator.Object,
//                _mockGetUserBookingsValidator.Object);

//            // 6. Заполняем тестовые данные
//            SeedTestData();
//        }

//        private void SetupValidators()
//        {
//            // Явно используем FluentValidation.Results.ValidationResult
//            var validationResult = new FluentValidation.Results.ValidationResult();

//            _mockCreateValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<CreateBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockUpdateValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<UpdateBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockHardDeleteValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<HardDeleteBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockSoftDeleteValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<SoftDeleteBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockGetBookingValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<GetBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockGetLastUserBookingValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<GetLastUserBookingDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);

//            _mockGetUserBookingsValidator
//                .Setup(v => v.ValidateAsync(It.IsAny<GetUserBookingsDto>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult);
//        }

//        private void SetupMockRepositories()
//        {
//            // Настраиваем RoomRepository
//            _mockRoomRepository
//                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
//                .Returns<Guid, CancellationToken>(async (id, ct) =>
//                {
//                    return await _dbContext.Rooms
//                        .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, ct);
//                });

//            // Настраиваем BookingRepository
//            _mockBookingRepository
//                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
//                .Returns<Guid, CancellationToken>(async (id, ct) =>
//                {
//                    return await _dbContext.Bookings
//                        .Include(b => b.Room)
//                        .Include(b => b.Payment)
//                        .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, ct);
//                });

//            _mockBookingRepository
//                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
//                .Returns<CancellationToken>(async (ct) =>
//                {
//                    return await _dbContext.Bookings
//                        .Where(b => !b.IsDeleted)
//                        .Include(b => b.User)
//                        .Include(b => b.Room)
//                        .Include(b => b.Payment)
//                        .ToListAsync(ct);
//                });

//            _mockBookingRepository
//                .Setup(r => r.GetAllByUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
//                .Returns<User, CancellationToken>(async (user, ct) =>
//                {
//                    return await _dbContext.Bookings
//                        .Where(b => b.UserId == user.Id && !b.IsDeleted)
//                        .Include(b => b.User)
//                        .Include(b => b.Room)
//                        .Include(b => b.Payment)
//                        .ToListAsync(ct);
//                });

//            _mockBookingRepository
//                .Setup(r => r.GetLastBookingByUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
//                .Returns<User, CancellationToken>(async (user, ct) =>
//                {
//                    return await _dbContext.Bookings
//                        .Where(b => b.UserId == user.Id && !b.IsDeleted)
//                        .OrderByDescending(b => b.CreatedAt)
//                        .Include(b => b.User)
//                        .Include(b => b.Room)
//                        .Include(b => b.Payment)
//                        .FirstOrDefaultAsync(ct);
//                });

//            _mockBookingRepository
//                .Setup(r => r.CreateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
//                .Callback<Booking, CancellationToken>((booking, ct) =>
//                {
//                    _dbContext.Bookings.Add(booking);
//                    _dbContext.SaveChanges();
//                })
//                .Returns(Task.CompletedTask);

//            _mockBookingRepository
//                .Setup(r => r.UpdateAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
//                .Callback<Booking, CancellationToken>((booking, ct) =>
//                {
//                    _dbContext.Bookings.Update(booking);
//                    _dbContext.SaveChanges();
//                })
//                .Returns(Task.CompletedTask);

//            _mockBookingRepository
//                .Setup(r => r.SoftDeleteAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
//                .Callback<Booking, CancellationToken>((booking, ct) =>
//                {
//                    booking.IsDeleted = true;
//                    booking.DeletedAt = DateTime.UtcNow;
//                    _dbContext.Bookings.Update(booking);
//                    _dbContext.SaveChanges();
//                })
//                .Returns(Task.CompletedTask);

//            _mockBookingRepository
//                .Setup(r => r.HardDeleteAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
//                .Callback<Booking, CancellationToken>((booking, ct) =>
//                {
//                    _dbContext.Bookings.Remove(booking);
//                    _dbContext.SaveChanges();
//                })
//                .Returns(Task.CompletedTask);
//        }

//        private void SeedTestData()
//        {
//            // Очищаем базу
//            _dbContext.Database.EnsureDeleted();
//            _dbContext.Database.EnsureCreated();

//            // Создаем UserType
//            var userType = new UserType
//            {
//                Id = Guid.NewGuid(),
//                Type = "Customer",
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            // Создаем User
//            var user = new User
//            {
//                Id = Guid.NewGuid(),
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "password123",
//                UserTypeId = userType.Id,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            // Создаем Room
//            var room = new Room
//            {
//                Id = Guid.NewGuid(),
//                Number = "101",
//                Class = RoomClass.Standard,
//                Description = "Standard room with sea view",
//                PricePerNight = 100.0m,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            // Создаем Booking
//            var bookingId = Guid.NewGuid();
//            var booking = new Booking
//            {
//                Id = bookingId,
//                CheckInDate = DateTime.UtcNow.AddDays(1),
//                CheckOutDate = DateTime.UtcNow.AddDays(3),
//                UserId = user.Id,
//                RoomId = room.Id,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            // Создаем Payment
//            var payment = new Payment
//            {
//                Id = Guid.NewGuid(),
//                TotalAmount = 200.0m,
//                PaymentDate = DateTime.UtcNow,
//                PaymentMethod = PaymentMethod.Card,
//                Status = PaymentStatus.Paid,
//                BookingId = bookingId,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            // Устанавливаем связи
//            booking.Payment = payment;
//            payment.Booking = booking;

//            // Сохраняем в базу
//            _dbContext.UserTypes.Add(userType);
//            _dbContext.Users.Add(user);
//            _dbContext.Rooms.Add(room);
//            _dbContext.Bookings.Add(booking);
//            _dbContext.Payments.Add(payment);
//            _dbContext.SaveChanges();
//        }

//        public void Dispose()
//        {
//            _dbContext?.Dispose();
//        }

//        [Fact]
//        public async Task CreateAsync_ValidData_ReturnsNewBookingId()
//        {
//            // Arrange
//            var user = await _dbContext.Users.FirstAsync();
//            var room = await _dbContext.Rooms.FirstAsync();

//            var createDto = new CreateBookingDto
//            {
//                UserId = user.Id,
//                RoomId = room.Id,
//                CheckInDate = DateTime.UtcNow.AddDays(10),
//                CheckOutDate = DateTime.UtcNow.AddDays(12)
//            };

//            // Act
//            var result = await _bookingService.CreateAsync(createDto, _cancellationToken);

//            // Assert
//            Assert.NotEqual(Guid.Empty, result);

//            // Проверяем, что бронирование создано в базе
//            var createdBooking = await _dbContext.Bookings
//                .FirstOrDefaultAsync(b => b.Id == result);

//            Assert.NotNull(createdBooking);
//            Assert.Equal(user.Id, createdBooking.UserId);
//            Assert.Equal(room.Id, createdBooking.RoomId);
//            Assert.False(createdBooking.IsDeleted);
//            Assert.NotNull(createdBooking.CreatedAt);
//        }

//        [Fact]
//        public async Task CreateAsync_UserNotFound_ReturnsEmptyGuid()
//        {
//            // Arrange
//            var room = await _dbContext.Rooms.FirstAsync();
//            var nonExistentUserId = Guid.NewGuid();

//            var createDto = new CreateBookingDto
//            {
//                UserId = nonExistentUserId,
//                RoomId = room.Id,
//                CheckInDate = DateTime.UtcNow.AddDays(1),
//                CheckOutDate = DateTime.UtcNow.AddDays(3)
//            };

//            // Act
//            var result = await _bookingService.CreateAsync(createDto, _cancellationToken);

//            // Assert
//            Assert.Equal(Guid.Empty, result);
//        }

//        [Fact]
//        public async Task CreateAsync_RoomNotFound_ReturnsEmptyGuid()
//        {
//            // Arrange
//            var user = await _dbContext.Users.FirstAsync();
//            var nonExistentRoomId = Guid.NewGuid();

//            var createDto = new CreateBookingDto
//            {
//                UserId = user.Id,
//                RoomId = nonExistentRoomId,
//                CheckInDate = DateTime.UtcNow.AddDays(1),
//                CheckOutDate = DateTime.UtcNow.AddDays(3)
//            };

//            // Act
//            var result = await _bookingService.CreateAsync(createDto, _cancellationToken);

//            // Assert
//            Assert.Equal(Guid.Empty, result);
//        }

//        [Fact]
//        public async Task GetByIdAsync_ExistingBooking_ReturnsBookingDetails()
//        {
//            // Arrange
//            var existingBooking = await _dbContext.Bookings
//                .Include(b => b.User)
//                .Include(b => b.Room)
//                .Include(b => b.Payment)
//                .FirstAsync();

//            var getDto = new GetBookingDto { Id = existingBooking.Id };

//            // Настраиваем маппер
//            var expectedVm = new BookingDetailsVm
//            {
//                Id = existingBooking.Id,
//                CheckInDate = existingBooking.CheckInDate,
//                CheckOutDate = existingBooking.CheckOutDate
//            };

//            _mockMapper
//                .Setup(m => m.Map<BookingDetailsVm>(It.Is<Booking>(b => b.Id == existingBooking.Id)))
//                .Returns(expectedVm);

//            // Act
//            var result = await _bookingService.GetByIdAsync(getDto, _cancellationToken);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(existingBooking.Id, result.Id);
//            Assert.Equal(existingBooking.CheckInDate, result.CheckInDate);
//            Assert.Equal(existingBooking.CheckOutDate, result.CheckOutDate);
//        }

//        [Fact]
//        public async Task GetByIdAsync_NonExistentBooking_ReturnsNull()
//        {
//            // Arrange
//            var nonExistentBookingId = Guid.NewGuid();
//            var getDto = new GetBookingDto { Id = nonExistentBookingId };

//            // Act
//            var result = await _bookingService.GetByIdAsync(getDto, _cancellationToken);

//            // Assert
//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task GetAllAsync_ReturnsAllNonDeletedBookings()
//        {
//            // Arrange
//            // Создаем еще одно бронирование
//            var user = await _dbContext.Users.FirstAsync();
//            var room = await _dbContext.Rooms.FirstAsync();

//            var newBooking = new Booking
//            {
//                Id = Guid.NewGuid(),
//                CheckInDate = DateTime.UtcNow.AddDays(5),
//                CheckOutDate = DateTime.UtcNow.AddDays(7),
//                UserId = user.Id,
//                RoomId = room.Id,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            await _dbContext.Bookings.AddAsync(newBooking);
//            await _dbContext.SaveChangesAsync();

//            // Настраиваем маппер
//            var bookings = await _dbContext.Bookings
//                .Where(b => !b.IsDeleted)
//                .Include(b => b.User)
//                .Include(b => b.Room)
//                .Include(b => b.Payment)
//                .ToListAsync();

//            var expectedLookups = bookings.Select(b => new BookingLookupDto { Id = b.Id }).ToList();

//            _mockMapper
//                .Setup(m => m.Map<IList<BookingLookupDto>>(It.IsAny<List<Booking>>()))
//                .Returns(expectedLookups);

//            // Act
//            var result = await _bookingService.GetAllAsync(_cancellationToken);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.BookingLookups.Count); // Оригинальное + новое
//            Assert.Contains(result.BookingLookups, b => b.Id == newBooking.Id);
//        }

//        [Fact]
//        public async Task UpdateAsync_ValidData_UpdatesBookingSuccessfully()
//        {
//            // Arrange
//            var booking = await _dbContext.Bookings.FirstAsync();
//            var user = await _dbContext.Users.FirstAsync();
//            var room = await _dbContext.Rooms.FirstAsync();

//            var updateDto = new UpdateBookingDto
//            {
//                Id = booking.Id,
//                UserId = user.Id,
//                RoomId = room.Id,
//                CheckInDate = DateTime.UtcNow.AddDays(15),
//                CheckOutDate = DateTime.UtcNow.AddDays(18)
//            };

//            // Act
//            await _bookingService.UpdateAsync(updateDto, _cancellationToken);

//            // Assert
//            var updatedBooking = await _dbContext.Bookings.FindAsync(booking.Id);
//            Assert.NotNull(updatedBooking);
//            Assert.Equal(updateDto.CheckInDate, updatedBooking.CheckInDate);
//            Assert.Equal(updateDto.CheckOutDate, updatedBooking.CheckOutDate);
//            Assert.NotNull(updatedBooking.UpdatedAt);
//        }

//        [Fact]
//        public async Task SoftDeleteAsync_SetsBookingAsDeleted()
//        {
//            // Arrange
//            var booking = await _dbContext.Bookings.FirstAsync();
//            var softDeleteDto = new SoftDeleteBookingDto { Id = booking.Id };

//            // Act
//            await _bookingService.SoftDeleteAsync(softDeleteDto, _cancellationToken);

//            // Assert
//            var deletedBooking = await _dbContext.Bookings.FindAsync(booking.Id);
//            Assert.NotNull(deletedBooking);
//            Assert.True(deletedBooking.IsDeleted);
//            Assert.NotNull(deletedBooking.DeletedAt);
//        }

//        [Fact]
//        public async Task HardDeleteAsync_RemovesBookingFromDatabase()
//        {
//            // Arrange
//            // Создаем новое бронирование для удаления
//            var user = await _dbContext.Users.FirstAsync();
//            var room = await _dbContext.Rooms.FirstAsync();

//            var bookingToDelete = new Booking
//            {
//                Id = Guid.NewGuid(),
//                CheckInDate = DateTime.UtcNow.AddDays(30),
//                CheckOutDate = DateTime.UtcNow.AddDays(33),
//                UserId = user.Id,
//                RoomId = room.Id,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = false
//            };

//            await _dbContext.Bookings.AddAsync(bookingToDelete);
//            await _dbContext.SaveChangesAsync();

//            var hardDeleteDto = new HardDeleteBookingDto { Id = bookingToDelete.Id };

//            // Act
//            await _bookingService.HardDeleteAsync(hardDeleteDto, _cancellationToken);

//            // Assert
//            var deletedBooking = await _dbContext.Bookings.FindAsync(bookingToDelete.Id);
//            Assert.Null(deletedBooking);
//        }

//        [Fact]
//        public async Task GetAllAsync_WithDeletedBooking_ReturnsOnlyNonDeleted()
//        {
//            // Arrange
//            var user = await _dbContext.Users.FirstAsync();
//            var room = await _dbContext.Rooms.FirstAsync();

//            // Создаем удаленное бронирование
//            var deletedBooking = new Booking
//            {
//                Id = Guid.NewGuid(),
//                CheckInDate = DateTime.UtcNow.AddDays(40),
//                CheckOutDate = DateTime.UtcNow.AddDays(43),
//                UserId = user.Id,
//                RoomId = room.Id,
//                CreatedAt = DateTime.UtcNow,
//                IsDeleted = true,
//                DeletedAt = DateTime.UtcNow
//            };

//            await _dbContext.Bookings.AddAsync(deletedBooking);
//            await _dbContext.SaveChangesAsync();

//            // Настраиваем маппер
//            var nonDeletedBookings = await _dbContext.Bookings
//                .Where(b => !b.IsDeleted)
//                .Include(b => b.User)
//                .Include(b => b.Room)
//                .Include(b => b.Payment)
//                .ToListAsync();

//            var expectedLookups = nonDeletedBookings.Select(b => new BookingLookupDto { Id = b.Id }).ToList();

//            _mockMapper
//                .Setup(m => m.Map<IList<BookingLookupDto>>(It.IsAny<List<Booking>>()))
//                .Returns(expectedLookups);

//            // Act
//            var result = await _bookingService.GetAllAsync(_cancellationToken);

//            // Assert
//            Assert.NotNull(result);
//            // Не должно включать удаленное бронирование
//            Assert.DoesNotContain(result.BookingLookups, b => b.Id == deletedBooking.Id);
//            // Должны получить только не удаленные
//            Assert.All(result.BookingLookups, lookup =>
//                Assert.Contains(nonDeletedBookings, b => b.Id == lookup.Id));
//        }
//    }
//}
