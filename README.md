## 📚 BookSwapLite
BookSwapLite is a small ASP.NET Core MVC web application that allows users to exchange books with others through swap requests.
This project is built to practice the main ASP.NET MVC concepts such as working with databases, creating CRUD pages, and building a clean Bootstrap interface.
The idea of the application is simple: you can add books, browse what others have posted, and request an exchange.
This project was created as a practice application for the ASP.NET Fundamentals course at SoftUni.

## 🚀 Features
User Registration and Login (ASP.NET Identity)
Browse all available books
Add, edit, and delete books (CRUD)
Request a book swap from another user
Approve or reject swap requests
View your sent swap requests
View swap requests for your own books

## 🛠️ Technologies Used
ASP.NET Core MVC (.NET 8)
Entity Framework Core
SQL Server
ASP.NET Identity
Bootstrap 5
Razor Views + Layout + Partial Views
Dependency Injection

## 📂 Project Structure
BookSwap.Data – Database models and EF Core configuration
BookSwap.Services – Business logic and services
BookSwap.Web – Controllers, Views, and UI

## 🧩 Entity Models
The project includes at least 3 main entities:
Book
Genre
SwapRequest
(plus Identity User model)

## ✅ CRUD Operations
Full CRUD functionality is implemented for the main entity:
Books
Create book
View all books
Edit book
Delete book

## 🗄️ Database Setup
The application uses SQL Server with Entity Framework Core migrations.

Apply migrations:
Update-Database
This will create the database and seed the initial genres.

## ⚙️ Configuration
Connection string is located in:

appsettings.json

Example:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookSwapLite;Trusted_Connection=True;TrustServerCertificate=True"
}

## ▶️ How to Run the Project
Clone the repository:

git clone https://github.com/Simona127/BookSwapLite.git
Open the solution in Visual Studio

Restore NuGet packages

Apply migrations:
Update-Database

Run the project: Ctrl + F5

## 📌 Navigation
All pages are accessible through the navigation bar, including:
Books
Add Book
Swap Requests
Requests for My Books

## 📄Seed Data
The app automatically adds several genres when the database is created, but books must be added manually through the UI.

Here are some example books that can be used for testing:

**The Great Gatsby**
Title: The Great Gatsby
Author: F. Scott Fitzgerald
Genre: Drama
Condition: Used - Good
Description: A classic novel about the American dream and lost love.

**Dune**
Title: Dune
Author: Frank Herbert
Genre: Science Fiction
Condition: Like New
Description: Epic sci-fi story about politics, power and destiny.

**Harry Potter and the Philosopher's Stone**
Title: Harry Potter and the Philosopher's Stone
Author: J.K. Rowling
Genre: Fantasy
Condition: New
Description: The magical beginning of Harry Potter’s journey.

**Gone Girl**
Title: Gone Girl
Author: Gillian Flynn
Genre: Thriller
Condition: Used - Acceptable
Description: A psychological thriller full of twists.

**Sherlock Holmes: The Complete Collection**
Title: Sherlock Holmes: The Complete Collection
Author: Arthur Conan Doyle
Genre: Mystery
Condition: Used - Good
Description: Classic detective mysteries featuring Sherlock Holmes.

##💻Testing Accounts
The app uses ASP.NET Identity, so users can create their own accounts locally for testing.
Example test user:
- Username: testuser@example.com
- Password: created locally during testing

##👩‍💻 Author
Developed by Simona Grachka.
