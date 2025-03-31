![image](https://github.com/user-attachments/assets/0de16cbf-2518-4a12-9c43-6268db185e93)# Setup Redis and Run .NET Project

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

### **Run .NET Project **
```sh
dotnet run
![image](https://github.com/user-attachments/assets/b0768a93-a255-40fa-a0c2-9ac0f69f8bf1)

```
