# 📚 BookSwapLite

**BookSwapLite** is a full-stack ASP.NET Core MVC web application for exchanging books between users through a structured swap request system.

The platform allows users to list books, browse available listings, send swap requests, and interact through reviews and favorites. The application follows a clean layered architecture and demonstrates best practices in ASP.NET Core development.

---

## 🚀 Features

### 🔐 Authentication & Authorization

* User registration and login (ASP.NET Identity)
* Role-based access (Admin area)
* Protected routes and actions

### 📚 Books Management

* Create, edit, delete books (CRUD)
* View all books
* Detailed book pages

### 🔄 Swap Requests System

* Send swap requests to other users
* Accept or reject requests
* Track:

  * Sent requests
  * Received requests

### ⭐ Favorites

* Add books to favorites
* View personal favorite books list

### 📝 Reviews System

* Add reviews
* View reviews for users

### 🛠️ Admin Panel

* Admin dashboard
* Management capabilities

---

## 🏗️ Architecture

The project follows a **layered architecture (Separation of Concerns)**:

### 📂 Project Structure

* **BookSwap.Data**

  * Entity configurations
  * EF Core migrations
  * ApplicationDbContext

* **BookSwap.Data.Models**

  * Book
  * Genre
  * SwapRequest
  * Review
  * Message
  * Favorite
  * ApplicationUser

* **BookSwap.Core**

  * Contracts (Interfaces)
  * Services (Business Logic)
  * ViewModels

* **BookSwapLite (Web)**

  * Controllers
  * Razor Views
  * Areas (Admin, Identity)
  * UI (Bootstrap)

* **BookSwap.Tests**

  * Unit tests

---

## 🧩 Design Principles

* Dependency Injection
* Separation of Concerns
* Service Layer Architecture
* Use of ViewModels
* Clean and maintainable code

---

## 🛠️ Technologies Used

* ASP.NET Core MVC (.NET 8)
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* Bootstrap 5
* Razor Views
* LINQ
* xUnit

---

## 🧪 Unit Testing

Includes tests for:

* BookService
* ReviewService
* SwapRequestService

---

## 🗄️ Database Setup

Using **Entity Framework Core (Code First)**

Run:

Update-Database

---

## ⚙️ Configuration

Located in:

appsettings.json

Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookSwapLite;Trusted_Connection=True;TrustServerCertificate=True"
}

---

## ▶️ How to Run

1. Clone repository:

git clone https://github.com/Simona127/BookSwapLite.git

2. Open in Visual Studio
3. Restore NuGet packages
4. Run migrations:

Update-Database

5. Start the project:

Ctrl + F5

---

## 📌 Key Functionalities

* Full CRUD for Books
* Swap request workflow
* Favorites system
* Reviews system
* Admin area
* Clean UI

---

## 📄 Seed Data

* Genres are seeded automatically
* Books are added via UI

---

## 📈 Future Improvements

* Notifications
* Chat system
* Pagination & filtering
* Image uploads
* Cloud deployment (Azure)

---

## 👩‍💻 Author

**Simona Grachka**

---

## 🏁 Project Purpose

This project demonstrates practical skills in:

* ASP.NET Core MVC
* Entity Framework Core
* Authentication & Authorization
* Clean Architecture
* Unit Testing

---
