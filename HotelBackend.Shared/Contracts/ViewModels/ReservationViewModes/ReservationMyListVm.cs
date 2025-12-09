namespace HotelBackend.Shared.Contracts.ViewModels.ReservationViewModes
{
    public class ReservationMyListVm
    {
        public IList<ReservationLookupDto> MyReservations { get; set; } = [];
    }
}
