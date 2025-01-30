# SmartHomeManager

Smart Home Manager is an intuitive application developed on ASP.NET Core, utilizing the framework's interface structure for service management. This design allows for easy integration and efficient control of smart home devices, including sensors, cameras, thermostats, and door locks. The app provides a unified platform to monitor and optimize your home’s security, comfort, and energy efficiency, ensuring seamless and flexible operation.

## Requirements

To run this project, the following requirements must be met:

- .NET 8 or later version.
- Microsoft SQL Server or a compatible database.

## Getting Started

Before you start the project, follow these steps:

###  Edit the `appsettings.json` File

You need to add the database connection string to the `appsettings.json` file used in the project.

Open the `appsettings.json` file and update the connection string as follows:

```json
  "ConnectionStrings": {
    "AuthConnString": "Server={SERVER_NAME};Database={DATABASE_NAME};Trusted_Connection=True;"
  },
```
- Replace {SERVER_NAME} with the name of your database server.
- Replace {DATABASE_NAME} with the name of your database. 

```json
  "SMTPSettings": {
    "SenderEmail": "SENDERMAIL",
    "Password": "PASSWORD",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
```
- Replace {SENDERMAIL} with your email address.
- Replace {PASSWORD} with your password.

### Add Migration and Update Database
If you are running the project for the first time, you will need to update the database by running the Entity Framework Core migration commands.

Run the following commands in the terminal or command prompt to add the migration and update the database:

#### Add the migration:
```
dotnet ef migrations add InitialCreate
```

#### Update the database:
```
dotnet ef database update
```

### Running the Application
To run the project, follow these steps:

Open the terminal or command prompt in the project directory.

Run the following command to start the application:
```
dotnet run
```
