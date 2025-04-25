## 📌 Introduction
This is a basic ASP.NET Core 9 CRUD (Create, Read, Update, Delete) web application that manages a list of persons with their respective details such as name, email, age, gender, country, and more. The app allows users to:

Add a new person
Edit existing entries
Delete a person
Search and filter persons based on specific fields
Sort entries based on different columns
The user interface is built using Razor views, styled with basic CSS, and leverages server-side rendering for all actions.

## 🛠 Tech Stack

Backend: ASP.NET Core 9, Entity Framework Core
Database: SQL Server (running in Docker)
Database Management: Azure Data Studio (MacOS)
Frontend: Razor Pages, HTML, CSS
Testing: xUnit for test-driven development
Containerization: Docker

## 🗂 Project Structure
bash
Copy
Edit
CRUDExample/
│
├── Program.cs                     # Main entry point
├── wwwroot/                       # Static files with seed data (countries.json, persons.json)
├── Controllers/
│   └── PersonsController.cs       # Handles all Person-related operations
├── Views/
│   └── Persons/                   # Razor views for creating, editing, Deleting.
├── appsettings.json              # Includes DB connection string
│
Entities/
│
├── PersonsDbContext.cs           # DB context
├── Migrations/                   # EF Core migration files
├── Entities/                     # Person, Country entity classes
│
Services/
│
├── PersonsService.cs             # Implements business logic
├── CountriesService.cs           # Implements business logic
├── Helpers/                      # ValidationHelper class implements ModelValidation method
│
ServiceContracts/
│
├── IPersonsService.cs            # Interface for persons service
├── ICountriesService.cs          # Interface for countries service
├── DTOs/                         # Data Transfer Objects:
│   ├── PersonResponse.cs
│   ├── PersonAddRequest.cs
│   ├── PersonUpdateRequest.cs
│   ├── CountryResponse.cs
│   └── CountryAddRequest.cs
│
CRUDTests/
│
├── PersonsServiceTest            # Unit tests using Xunit
├── CountriesServiceTest          # Unit tests using Xunit


## ▶️ Running the Projec

**Prerequisites:**
- .NET 9 SDK
- Docker
- Azure Data Studio
- VS Code with C# extensions and EF Core Tools

**1. Set up SQL Server in Docker with these command:**
    - docker pull mcr.microsoft.com/mssql/server:2022-latest
    - docker run --name sql1 -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Ousama12345@.." -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
**3. Connect via Azure Data Studio:**
    - Server name: localhost
    - Username: sa
    - Password: Ousama12345@..

**3. Update appsettings.json with the connection string:**
    - "ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=CrudDb;User Id=sa;Password=Ousama12345@..;Encrypt=False;"
}

**4. Apply Migrations with these command lines:**
    - dotnet ef migrations add InitialMigration --project ./Entities --startup-project ./CRUDExample
    - dotnet ef database update --project ./Entities --startup-project ./CRUDExample
**5. Run the app:**
    - dontnet run


## ✅ Usage

**Once the app is running:**
Navigate to /persons in your browser.
Use the interface to create, edit, delete, search, and sort through persons.
Data is retrieved and displayed using the Index action in PersonsController.

## 🔍 API Logic

Controllers interact with IPersonsService and ICountriesService via dependency injection.
All business logic is handled in the Services layer.
DTOs are used to transfer data safely and clearly between layers.
Entities hold model definitions and database-related classes like PersonsDbContext.

## 🧪 Testing

The project follows a TDD approach using xUnit. Unit tests are found in the CRUDTests folder and test the core business logic in the service layer.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

