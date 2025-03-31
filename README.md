# Setup Redis and Run .NET Project

## **1. Install Redis**

### **Windows**
Using Docker:
```sh
docker run --name redis-server -d -p 6379:6379 redis
```
Using Docker-Compose:
```sh
docker compose up -d
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
git clone https://github.com/Newt1712/BookingProblems.git
cd BookingProblems
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

### **Run .NET Project**
```sh
cd KV.Q2
dotnet run
```
![image](https://github.com/user-attachments/assets/e16a3380-441e-47fd-a3f7-a0a9ec8003c8)


### **Final Result**
![image](https://github.com/user-attachments/assets/455f1228-e523-4bd2-915d-a71c1d883d3e)
