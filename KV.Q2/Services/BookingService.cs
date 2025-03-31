using KV.Q2.Settings;
using StackExchange.Redis;

namespace KV.Q2.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDatabase _cacheDb;

        public BookingService(IConnectionMultiplexer cacheDb)
        {
            _cacheDb = cacheDb.GetDatabase();
        }

        public async Task<bool> BookRoom(int roomId, DateTime checkIn, DateTime checkOut)
        {
            string redisKey = $"booking:{checkIn:yyyyMMdd}:{roomId}";
            var data = await _cacheDb.StringGetAsync(redisKey);

            int timeSlot = AppSettings.TimeSlot;
            int gap = 60 / timeSlot;
            int bitSetLength = 24 * gap;

            string bitset = data.IsNullOrEmpty ? new string('0', bitSetLength) : data.ToString();
            int startBit = (checkIn.Hour * gap) + (checkIn.Minute >= timeSlot ? 1 : 0);
            int endBit = (checkOut.Hour * gap) + (checkOut.Minute >= timeSlot ? 1 : 0);

            var bitArray = bitset.ToCharArray();
            for (int i = startBit; i < endBit; i++)
            {
                bitArray[i] = '1';
            }

            return await _cacheDb.StringSetAsync(redisKey, new string(bitArray), TimeSpan.FromDays(30));
        }

        public async Task<bool> CancelBooking(int roomId, DateTime checkIn, DateTime checkOut)
        {
            string redisKey = $"booking:{checkIn:yyyyMMdd}:{roomId}";
            var data = await _cacheDb.StringGetAsync(redisKey);

            if (data.IsNullOrEmpty) return false;

            char[] bitset = data.ToString().ToCharArray();

            int timeSlot = AppSettings.TimeSlot;
            int gap = 60 / timeSlot;

            int startBit = (checkIn.Hour * gap) + (checkIn.Minute >= timeSlot ? 1 : 0);
            int endBit = (checkOut.Hour * gap) + (checkOut.Minute >= timeSlot ? 1 : 0);

            for (int i = startBit; i < endBit; i++)
            {
                bitset[i] = '0';
            }

            return await _cacheDb.StringSetAsync(redisKey, new string(bitset), TimeSpan.FromDays(30));
        }

        public async Task<bool> IsRoomAvailableByDay(int roomId, DateTime date)
        {
            string redisKey = $"booking:{date:yyyyMMdd}:{roomId}";
            var data = await _cacheDb.StringGetAsync(redisKey);
            return data.IsNullOrEmpty || !data.ToString().Contains('1');
        }

        public async Task<bool> IsRoomAvailableByTimeRange(int roomId, DateTime checkIn, DateTime checkOut)
        {
            string redisKey = $"booking:{checkIn:yyyyMMdd}:{roomId}";
            var data = await _cacheDb.StringGetAsync(redisKey);
            if (data.IsNullOrEmpty) return true;

            int timeSlot = AppSettings.TimeSlot;
            int gap = 60 / timeSlot;

            int startBit = (checkIn.Hour * gap) + (checkIn.Minute >= timeSlot ? 1 : 0);
            int endBit = (checkOut.Hour * gap) + (checkOut.Minute >= timeSlot ? 1 : 0);

            string bitset = data.ToString();
            for (int i = startBit; i < endBit; i++)
            {
                if (bitset[i] == '1') return false;
            }
            return true;
        }
    }
}
