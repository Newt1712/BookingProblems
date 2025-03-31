namespace KV.Q2.Services
{
    public interface IBookingService
    {
        Task<bool> BookRoom(int roomId, DateTime checkIn, DateTime checkOut);
        Task<bool> CancelBooking(int roomId, DateTime checkIn, DateTime checkOut);

        Task<bool> IsRoomAvailableByDay(int roomId, DateTime date);
        Task<bool> IsRoomAvailableByTimeRange(int roomId, DateTime checkIn, DateTime checkOut);

    }
}
