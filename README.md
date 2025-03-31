# Setup Redis and Run .NET Project

## **1. Install Redis**

### **Windows**
Using Docker:
```sh
docker run --name redis-server -d -p 6379:6379 redis
```
Or install Redis for Windows from [Memurai](https://www.memurai.com/) (since official Redis is not supported natively on Windows).

### **Linux & MacOS**
Using Homebrew (MacOS):
```sh
brew install redis
brew services start redis
```

Using APT (Ubuntu/Debian):
```sh
sudo apt update
sudo apt install redis-server
sudo systemctl start redis
sudo systemctl enable redis
```

### **Verify Redis is Running**
```sh
redis-cli ping
```
Should return: `PONG`

---

## **2. Set Up .NET Project**

### **Clone the Repository**
```sh
git clone https://github.com/your-repo/your-project.git
cd your-project
```

### **Install Dependencies**
```sh
dotnet restore
```

### **Configure Redis Connection in .NET**
Edit `appsettings.json`:
```json
{
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

### **Update `Program.cs`**
```csharp
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]));

var app = builder.Build();

app.MapGet("/health", () => "API is running");

app.Run();
```

---

## **3. Run the .NET Project**
```sh
dotnet run
```

The API should now be running on `http://localhost:5000` or `https://localhost:5001`.

---

## **4. Test Redis Connection**
Use `redis-cli`:
```sh
redis-cli
127.0.0.1:6379> SET test "Hello, Redis!"
127.0.0.1:6379> GET test
```
Should return: `Hello, Redis!`

Or test in .NET:
```csharp
var redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();
db.StringSet("test", "Hello, Redis!");
Console.WriteLine(db.StringGet("test"));
```

---

## **5. Run API Endpoints**
### **Check Health**
```sh
curl http://localhost:5000/health
```
Response:
```json
"API is running"
```

### **Run Booking API**
```sh
curl -X POST "http://localhost:5000/api/booking/cancel-booking" -H "Content-Type: application/json" -d '{"roomId": "A101", "date": "2024-01-01", "checkIn": "13:00", "checkOut": "15:00"}'
```

---

## **6. Stop and Remove Redis (Optional)**
```sh
docker stop redis-server && docker rm redis-server
```

