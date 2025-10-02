# Hospital Management System API

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![Pattern](https://img.shields.io/badge/Pattern-CQRS%20%26%20Mediator-orange)](https://martinfowler.com/bliki/CQRS.html)
[![License](https://img.shields.io/badge/License-MIT-green)](./LICENSE)

A comprehensive, professional-grade backend API for a modern hospital management system. This project is built using **.NET 8** and strictly follows **Clean Architecture** principles, making it a robust, scalable, and maintainable template for enterprise-level applications.

The API covers a wide range of hospital operations, from user management and appointments to a fully transactional pharmacy inventory system, complete with background jobs and an AI-powered chatbot.

## ✨ Key Features

* **Role-Based Security:** Complete JWT-based authentication and authorization system for different user roles (Admin, Doctor, Patient, etc.).
* **Full User Management:** User registration with email confirmation, login, change password, and admin-level user/role management.
* **🗓️ Advanced Appointment & Prescription System:** A complete workflow for booking, managing, and cancelling appointments. The system features an automated **recurring background job (Hangfire)** that checks every 15 minutes for upcoming appointments and sends email reminders to patients one hour beforehand to reduce no-shows.
* **Transactional Pharmacy Module:**
    * A robust medicine inventory system.
    * A complete audit trail for every stock change (`StockAdjustment`) and sale (`DispenseLog`).
    * Smart business logic for dispensing prescriptions (FEFO principle).
* **AI-Powered Chatbot:** An integrated chatbot (using a provider like OpenRouter) to provide patients with initial medical triage suggestions.
* **Asynchronous Background Jobs:** Utilizes Hangfire for reliable, "fire-and-forget" and scheduled tasks like:
    * Sending registration and reminder emails.
    * Notifying managers of low stock levels.
    * Automatic appointment reminders.
* **Advanced API Features:**
    * Global error handling middleware for consistent error responses.
    * FluentValidation for all incoming requests, integrated via a MediatR pipeline.
    * Secure server-side logout using token blacklisting (with Redis or In-Memory Cache).
* **📝 Structured Logging:** Implemented **Serilog** for structured, production-ready logging to files and the console, providing a trail and easy debugging.
  
## 🏗️ Architectural Overview

This project is a practical implementation of **Clean Architecture**, emphasizing a clear separation of concerns and the Dependency Rule.

```
+------------------------------------------------------------------+
|                      API (Presentation Layer)                    |
|       (Controllers, Middleware, DI Configuration)                |
+------------------------------------------------------------------+
                               |
                               v
+------------------------------------------------------------------+
|                    Application Layer                             |  <-- Infrastructure Layer
|  (Business Logic, Commands, Queries, DTOs, Interfaces, Handlers) |      Implements Interfaces
+------------------------------------------------------------------+
                               |
                               v
+------------------------------------------------------------------+
|                       Domain Layer                               |
|       (Entities, Enums, Core Business Rules)                     |
+------------------------------------------------------------------+
```

* **Domain Layer:** The core of the application. It contains the enterprise business logic and entities (`Doctor`, `Patient`, `Appointment`, etc.). It has **zero dependencies** on any other layer.
* **Application Layer:** Contains the application-specific business logic. It orchestrates the domain entities to perform use cases. This is where all the CQRS **Commands**, **Queries**, and **Handlers** live. It depends only on the Domain layer.
* **Infrastructure Layer:** Provides implementations for the interfaces defined in the Application layer. It handles all external concerns like database access (via **EF Core**, **Repository & Unit of Work patterns**), email sending, AI service calls, and background job processing.
* **API Layer:** The entry point to the application. It contains the **Controllers** which are kept "thin" by delegating all work to the Application layer via **MediatR**. It also contains all infrastructure setup like dependency injection, authentication configuration, and middleware.

## 🛠️ Tech Stack & Key Packages

* **Framework:** .NET 8, ASP.NET Core
* **Architecture:** Clean Architecture, CQRS, RESTful API Design
* **Data Access:** Entity Framework Core 8, SQL Server
* **Authentication & Authorization:** ASP.NET Core Identity, JWT
* **Business Logic:** MediatR
* **Validation:** FluentValidation
* **Background Jobs:** Hangfire
* **Caching:** StackExchange.Redis / IMemoryCache
* **AI Integration:** Azure.AI.OpenAI (for OpenAI-compatible APIs like OpenRouter)
* **Logging:** Serilog

## 🏁 Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or other editions)
* (Optional) [Redis Desktop Manager](https://redis.io/docs/ui/clients/) or other Redis client for the blacklist feature.

### Installation & Configuration

1.  **Clone the repository:**
    ```sh
    git clone [https://github.com/MohammedMetw/Hospital-Management-System-API.NET.git](https://github.com/MohammedMetw/Hospital-Management-System-API.NET.git)
    ```
2.  **Navigate to the project directory:**
    ```sh
    cd Hospital-Management-System-API.NET
    ```
3.  **Configure your settings:**
    * In the `Hospital.API` project, rename `appsettings.Development.json.template` to `appsettings.Development.json`.
    * Open the new file and update the following sections with your own values:
        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=...;Database=HospitalDB;...",
            "Redis": "localhost:6379"
          },
          "JwtSettings": {
            "Key": "YOUR_SUPER_SECRET_KEY_THAT_IS_LONG_AND_COMPLEX",
            "Issuer": "https://localhost:5001",
            "Audience": "https://localhost:5001",
            "ExpirationInDays": 7
          },
          "EmailSettings": {
            "SmtpServer": "smtp.gmail.com",
            "Port": 587,
            "SenderName": "Your Hospital System",
            "SenderEmail": "your-email@gmail.com",
            "Password": "your-gmail-app-password"
          },
          "AiSettings": {
            "OpenRouterApiKey": "sk-or-...",
            "OpenRouterBaseUrl": "[https://openrouter.ai/api/v1](https://openrouter.ai/api/v1)"
          }
        }
        ```
4.  **Apply Database Migrations:**
    * In Visual Studio's Package Manager Console, select `Hospital.Infrastructure` as the default project.
    * Run the command:
        ```powershell
        Update-Database
        ```
5.  **Run the application:**
    * Set `Hospital.API` as the startup project and run it (F5 or `dotnet run`).
    * The API will be available at `http://localhost:5151` (or a similar address), and the Swagger UI will be at `/swagger`.

## 🔌 API Endpoints

The API provides a full suite of RESTful endpoints for managing the hospital.

* `POST /api/auth/register` - Register a new patient account.
* `POST /api/auth/login` - Log in to get a JWT.
* `GET /api/doctors` - Get a list of all doctors.
* `GET /api/doctors/{id}/patients` - Get all patients for a specific doctor.
* `POST /api/appointments` - Book a new appointment.
* `POST /api/prescriptions` - Create a new prescription for an appointment.
* `POST /api/medicine/dispense` - Fulfill a prescription from the pharmacy.
* `GET /api/medicine/{id}/history` - Get the complete audit trail for a medicine batch.
* `POST /api/chat` - Interact with the AI triage chatbot.

...and many more. Explore the complete API using the Swagger UI.
