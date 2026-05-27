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