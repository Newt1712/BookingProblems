namespace KV.Q2.Entities
{
    public class BookingRoom
    {
        public int BookingId { get; set; }  // Unique ID for the booking
        public int RoomId { get; set; }  // Room Number or ID
        public DateTime BookingDate { get; set; }  // Store Date Separately (yyyy-MM-dd)
        public TimeSpan? StartTime { get; set; }  // Start Time of Booking
        public TimeSpan? EndTime { get; set; }  // End Time of Booking
        public int CustomerId{ get; set; }  
        public BookingStatus Status { get; set; }  // Status: Confirmed, Pending, Canceled
    }

    public enum BookingStatus
    {
        Confirmed,
        Pending,
        Canceled
    }
}
