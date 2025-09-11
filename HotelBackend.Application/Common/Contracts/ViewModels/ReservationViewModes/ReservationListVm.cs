namespace HotelBackend.Application.Common.Contracts.ViewModels.ReservationViewModes
{
    public class ReservationListVm
    {
        public IList<ReservationLookupDto> Reservations { get; set; } = [];
    }
}
