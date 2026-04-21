# 📚 BookSwapLite

**BookSwapLite** is a full-stack ASP.NET Core MVC web application for exchanging books between users through a structured swap request system.

The application allows users to add books, browse listings, send swap requests, mark favorites, and leave reviews. It follows a clean layered architecture and demonstrates best practices in ASP.NET Core development.

---

## 🌐 Live Demo

The application is deployed on Azure:

https://bookswap-simona-2026-hqchdvd9hvdwgaft.westeurope-01.azurewebsites.net/

---

## 🚀 Features

### 🔐 Authentication & Authorization

* User registration and login (ASP.NET Identity)
* Role-based authorization
* Admin area with restricted access

### 📚 Books Management

* Create, edit, delete books (CRUD)
* View all books
* Book details page

### 🔄 Swap Requests

* Send swap requests
* Accept / reject requests
* View:

  * Sent requests
  * Requests for your books

### ⭐ Favorites

* Add books to favorites
* View favorite books list

### 📝 Reviews

* Add reviews
* View reviews for users

### ⚠️ Error Handling

* Custom error pages:

  * 404 Not Found
  * 500 Internal Server Error

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
  * Views (Razor)
  * Areas (Admin, Identity)
  * Static files (wwwroot)

* **BookSwap.Tests**

  * Unit tests for services

---

## 🧩 Design Principles

* Dependency Injection
* Separation of Concerns
* Service Layer Pattern
* ViewModels usage
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

The project includes unit tests for:

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
  "DefaultConnection": "Server=localhost;Database=BookSwapLite;Trusted_Connection=True;TrustServerCertificate=True",
  "ApplicationDbContextConnection": "Server=(localdb)\\mssqllocaldb;Database=BookSwapLite;Trusted_Connection=True;MultipleActiveResultSets=true"
}

---

## ▶️ How to Run

1. Clone the repository:

git clone https://github.com/Simona127/BookSwapLite.git

2. Open in Visual Studio
3. Restore NuGet packages
4. Apply migrations:

Update-Database

5. Run the project:

Ctrl + F5

---

## 📌 Key Functionalities

* Full CRUD operations
* Swap request workflow
* Favorites system
* Reviews system
* Admin area
* Custom error handling
* Unit testing

---

## 📄 Seed Data

* Genres are seeded automatically
* Books are added via the UI

---

## 📈 Future Improvements

* Real-time notifications
* Chat system (Message entity ready)
* Pagination & filtering
* Image upload for books
* Cloud deployment (Azure)

---

## 👩‍💻 Author

**Simona Grachka**

---

## 🏁 Project Purpose

This project demonstrates:

* ASP.NET Core MVC architecture
* Entity Framework Core usage
* Authentication & Authorization
* Clean architecture principles
* Unit testing

---
