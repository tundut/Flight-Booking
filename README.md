# ✈️ Flight Booking API

A backend RESTful API built with ASP.NET Core for a simplified flight booking system.  
This project demonstrates backend development skills including REST API design, database modeling, authentication, and clean architecture principles.

---

## 🚀 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- JWT Authentication (planned / optional)
- C#

---

## 📌 Features

### 👤 User Features

- Register / Login
- Search available flights
- Create booking
- View booking history

### 🛠 Admin Features

- Create / Update / Delete flights
- Manage flight schedules

### ✈️ Flight Management

- Flight listing
- Search by departure, destination, date
- Pricing management

### 🎟 Booking System

- Create booking with passenger info
- Store booking history
- Simple booking status tracking

---

## 🗄 Database Design

The system uses a relational database with the following main tables:

- Users
- Flights
- Bookings
- Passengers

Relationships:

- One User → Many Bookings
- One Flight → Many Bookings
- One Booking → Many Passengers

---

## 🏗 Project Structure

FlightBooking
│
├── Controllers
├── Services
├── Models
├── DTOs
├── Data
├── Migrations
├── Interfaces
├── Middleware
└── Program.cs

---

## 📝 Hướng dẫn bổ sung (Tiếng Việt)

### Hướng dẫn cài đặt

- Yêu cầu: .NET 8 SDK, SQL Server (hoặc Docker SQL), `dotnet-ef` để chạy migration.
- Khôi phục gói và build:

```powershell
dotnet restore
dotnet build
```

### Cấu hình

- Cấu hình kết nối cơ sở dữ liệu nằm trong `appsettings.json` hoặc `appsettings.Development.json` dưới `ConnectionStrings:DefaultConnection`.
- Nếu sử dụng JWT, cấu hình khóa/issuer/audience trong `appsettings` (khóa: `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`).

### Khởi tạo cơ sở dữ liệu

- Áp dụng migration:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update --project backend
```

### Chạy ứng dụng

- Chạy backend (từ thư mục gốc hoặc `backend`):

```powershell
dotnet run --project backend
```

- Swagger UI sẽ khả dụng tại `https://localhost:{port}/swagger` để thử nghiệm API.

### API chính (tổng quan)

- `POST /api/auth/register` — Đăng ký người dùng
- `POST /api/auth/login` — Đăng nhập, trả về token
- `GET /api/flights` — Lấy danh sách chuyến bay
- `POST /api/flights` — (Admin) Tạo chuyến bay mới
- `POST /api/bookings` — Tạo booking
- `GET /api/bookings/{id}` — Lấy thông tin booking
- `POST /api/payments` — Tạo thanh toán

> Lưu ý: đường dẫn chính xác có thể bắt đầu bằng `api/` tùy cài đặt routing trong controller.

### Bảo mật

- API hỗ trợ JWT để bảo vệ endpoint; thêm header `Authorization: Bearer {token}` khi gọi các endpoint yêu cầu xác thực.

### Kiểm thử

- Sử dụng Swagger hoặc Postman để test các endpoint. Nếu cần, có thể thêm test dựa trên xUnit cho service layer.

### Đóng góp

- Mọi PR và issue đều được hoan nghênh. Vui lòng mở issue mô tả lỗi / feature trước khi gửi PR.

### Liên hệ

- Người phát triển: thêm thông tin liên hệ hoặc email nếu cần.

---

## 📝 English Supplement

### Overview

This project is a RESTful backend API for a simple flight booking system, implemented with ASP.NET Core. It provides user authentication, flight management, booking, and payment endpoints. The API uses PostgreSQL via Npgsql (configured in `Program.cs`) and supports JWT authentication and Swagger for API exploration.

### Requirements

- .NET 8 SDK
- PostgreSQL (or a Docker container running Postgres)
- `dotnet-ef` tool for migrations

### Quick Setup

1. Restore and build:

```powershell
dotnet restore
dotnet build
```

2. Configure connection string and JWT in `appsettings.json` or `appsettings.Development.json`:

- `ConnectionStrings:DefaultConnection` — PostgreSQL connection string
- `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` — values for JWT token validation

3. Apply migrations:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef database update --project backend
```

4. Run the backend:

```powershell
dotnet run --project backend
```

Swagger UI will be available at `https://localhost:{port}/swagger`.

### CORS

The application allows requests from the frontend origin `http://localhost:5173` by default (see `Program.cs` CORS policy `AllowFrontend`). Update as needed for deployment.

### Main API Endpoints

- `POST /api/auth/register` — Register a new user (public)
- `POST /api/auth/login` — Log in and receive JWT (public)
- `GET /api/flight` — Get all flights (public)
- `GET /api/flight/{id}` — Get flight by id (public)
- `POST /api/flight` — Create a flight (Admin only — `Authorize(Roles = "Admin")`)
- `DELETE /api/flight/{id}` — Delete a flight (Admin only)
- `GET /api/flight/search/{from}/{to}` — Search flights by origin/destination (public)
- `GET /api/booking/me` — Get current user's bookings (Authenticated)
- `GET /api/booking/{id}` — Get booking by id (Authenticated)
- `POST /api/booking` — Create a booking (Authenticated)
- `DELETE /api/booking/{id}` — Delete a booking (Authenticated)
- `POST /api/payment` — Create a payment for a booking (Authenticated)
- `GET /api/payment/booking/{bookingId}` — Get payment by booking id (Authenticated)

Authentication: include header `Authorization: Bearer {token}` for protected endpoints.

### Testing

- Use Swagger UI or Postman to exercise endpoints. Create a user, log in to obtain a JWT, and include it in subsequent requests.

### Contributing

- Issues and PRs are welcome. Please open an issue describing the change before submitting a PR.

### Next steps / Suggestions

- Add automated tests (xUnit) for services and controllers.
- Add seed data for development environment.
- Provide Postman collection or example HTTP requests in the repo.
