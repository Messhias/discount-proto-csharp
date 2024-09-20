# gRPC Project with .NET 8.0

This project is a **gRPC service** built using **.NET 8.0**, featuring the generation and usage of discount codes, with support for unit testing and an in-memory database.

## Requirements

- **.NET 8.0 SDK** (Ensure that it is installed on your machine)
- **SQLite** (if applicable)

# Be sure you're inside the GrpcService folder.

## Steps to Run the Project

Before running the project, it is **mandatory** to apply the Entity Framework Core migrations to set up the database schema correctly.

### 1. Applying Migrations

Ensure that the database schema is up to date by running the migrations.

#### Steps:

1. Open a terminal in the root of your project directory (where the `.csproj` file is located).
2. Run the following command to apply the migrations:
```   
dotnet ef database update --project GrpcService
```
This will apply any pending migrations and create the database (if using SQLite or another supported database).

If migrations haven't been created yet, you can generate them by running:

```
dotnet ef migrations add InitialCreate --project GrpcService
```
***Note: Ensure the dotnet-ef tool is installed globally if needed:***
```
dotnet tool install --global dotnet-ef
```

### 2. Running the Project (Using HTTP instead of HTTPS)

By default, the project is configured to run on HTTPS 
(`https://localhost:5001), but for local development, it is recommended to use HTTP (`http://localhost:5000`).

How to Force the Project to Use HTTP:
1. Open the `appsettings.json` file located in the `GrpcService` folder. Or create one by
`example.appsettings.json`
2. Save the changes and proceed to run the project.

### 3. Running the Project with Various Tools
The project can be run using VS Code, JetBrains Rider, Visual Studio, or directly from the terminal. Below are the steps to follow.

**VS Code**
1. Open the project in VS Code.

2. Open the integrated terminal in VS Code (Terminal > New Terminal).

3. Restore NuGet packages:
```
dotnet restore
```
4. Apply the migrations (as described above).
5. Run the project:
```
dotnet run
```

**JetBrains Rider**

1. Open the project in Rider.
2. Restore NuGet packages by running:
```
dotnet restore
```
3. Apply the migrations (as described above).
4. Run the project by selecting your run configuration and starting the application.

If you running it in localhost make sure the project is configured to run on HTTP (`http://localhost:5000`).
You shouldn't have any issues with that because is already configured in the 
``appsettings.json``.
