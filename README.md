# Class Attendance System

This is a web-based Class Attendance System built with ASP.NET Core. The application allows teachers and administrators to manage student attendance efficiently.

## Features
- Teacher and Admin login
- Manage students, teachers, and sections
- Take, edit, and view attendance records
- Admin dashboard for managing users and sections
- Secure authentication and role-based access

## Project Structure
- `Controllers/` — MVC controllers for handling requests
- `Models/` — Entity models for database tables
- `Views/` — Razor views for UI
- `Data/` — Database context and configuration
- `Migrations/` — Entity Framework Core migrations
- `wwwroot/` — Static files (CSS, JS, images, libraries)

## Getting Started
1. Clone the repository:
   ```bash
   git clone <your-repo-url>
   ```
2. Open the solution in Visual Studio or VS Code.
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Open your browser and navigate to `https://localhost:5001` (or the port shown in the terminal).

## Configuration
- Update `appsettings.json` for your database connection string and other settings.
- Use `appsettings.Development.json` for local development overrides.

## Screenshots
_Add screenshots here. You can add images to this section later._

## License
This project is for educational purposes.
