# Product API – Technical Assessment

## Project Overview

This project is a RESTful Backend API developed for managing products. The API supports CRUD operations and follows clean architecture principles, separation of concerns, and industry best practices.

The application allows users to create, retrieve, update, and delete product information through REST API endpoints.

The project also includes authentication using JWT, database integration using SQL Server and Entity Framework Core, Swagger documentation, unit testing, and Docker support.

---

## Architecture

The project follows a layered architecture to maintain separation of concerns and improve maintainability.

```text
Client
   │
   ▼
API Layer
   │
   ▼
Service Layer
   │
   ▼
Repository Layer
   │
   ▼
Entity Framework Core
   │
   ▼
SQL Server Database
```

### Layers

#### API Layer

Responsible for:

- Receiving HTTP requests
- Returning HTTP responses
- Authentication and authorization
- Swagger configuration
- Request validation

#### Service Layer

Responsible for:

- Business logic
- Product validation
- Processing application rules

#### Repository Layer

Responsible for:

- Database operations
- CRUD operations
- Communication with Entity Framework Core

#### Data Layer

Responsible for:

- Database context
- Entity configuration
- Database migrations

---

## Technologies

The following technologies are used in this project:

- .NET 8
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI
- xUnit
- Moq
- Docker
- Docker Compose
- Git and GitHub

---

## Prerequisites

Before running the project, make sure the following software is installed:

- .NET 8 SDK
- SQL Server or SQL Server Express
- SQL Server Management Studio
- Visual Studio or Visual Studio Code
- Git
- Docker Desktop (optional)

---

# Running Locally

Follow the steps below to run the application locally.

## Step 1: Clone the Repository

```bash
git clone YOUR_REPOSITORY_URL
```

Move into the project folder:

```bash
cd product-api-technical-assessment
```

---

## Step 2: Restore Dependencies

Run:

```bash
dotnet restore
```

---

## Step 3: Build the Solution

Run:

```bash
dotnet build
```

---

## Step 4: Configure SQL Server

Create a database named:

```text
ProductDb
```

Update the connection string in your configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

If you are using SQL Server Express:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Do not commit real production credentials to the repository.

---

# Database Migration and Setup

Install the Entity Framework Core tools if they are not already installed:

```bash
dotnet tool install --global dotnet-ef
```

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply the migration to create the database:

```bash
dotnet ef database update
```

You can verify the database using SQL Server Management Studio.

The database should contain the required tables after the migration is successfully applied.

---

# Running the API

Navigate to the API project:

```bash
cd src/ProductApi.API
```

Run the application:

```bash
dotnet run
```

The API will start on a local URL similar to:

```text
http://localhost:5000
```

or:

```text
https://localhost:5001
```

The exact port may vary depending on your launch settings.

---

# API Endpoints

## Product Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/products | Get all products |
| GET | /api/products/{id} | Get product by ID |
| POST | /api/products | Create a new product |
| PUT | /api/products/{id} | Update an existing product |
| DELETE | /api/products/{id} | Delete a product |

### Example Product Request

```json
{
  "name": "Laptop",
  "description": "High performance laptop",
  "price": 75000,
  "stockQuantity": 10
}
```

---

# Authentication

The API uses JWT authentication.

To access protected endpoints:

1. Register a user or use an existing user.
2. Login using the authentication endpoint.
3. Receive a JWT access token.
4. Add the token to the Authorization header.

Example:

```text
Authorization: Bearer YOUR_JWT_TOKEN
```

Protected API endpoints require a valid JWT token.

---

# Swagger

Swagger is used for API documentation and testing.

After running the application, open:

```text
https://localhost:PORT/swagger
```

Swagger allows you to:

- View available API endpoints
- View request and response models
- Test API endpoints
- Add JWT authorization tokens

To authorize using JWT in Swagger:

1. Login and copy the JWT token.
2. Click the **Authorize** button.
3. Enter:

```text
Bearer YOUR_JWT_TOKEN
```

4. Click **Authorize**.

---

# Running Tests

Navigate to the solution root and run:

```bash
dotnet test
```

The test project includes unit tests for business logic and other application components.

Example:

```bash
dotnet test tests/ProductApi.Tests
```

---

# Docker Setup

Docker can be used to run the API inside a container.

Make sure Docker Desktop is running.

Build and start the containers:

```bash
docker-compose up --build
```

To run in detached mode:

```bash
docker-compose up -d --build
```

To stop the containers:

```bash
docker-compose down
```

---

# Configuration

Application configuration can be managed using:

- appsettings.json
- Environment variables
- User secrets

Sensitive information should not be committed to the repository.

Examples of sensitive information include:

- Database passwords
- JWT production keys
- API keys
- Refresh tokens

Environment variables can be used for production configuration.

Example:

```text
ConnectionStrings__DefaultConnection
```

For local development, .NET User Secrets can also be used.

Initialize user secrets:

```bash
dotnet user-secrets init
```

Add a connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"
```

---

# Project Structure

```text
product-api-technical-assessment
│
├── src
│   └── ProductApi.API
│
├── tests
│   └── ProductApi.Tests
│
├── docker-compose.yml
├── .gitignore
├── README.md
└── product-api-technical-assessment.sln
```

---

# Future Improvements

Possible future improvements include:

- Refresh token implementation
- Role-based authorization
- Global exception handling middleware
- FluentValidation
- Pagination
- Filtering and sorting
- API versioning
- Rate limiting
- Redis caching
- Structured logging
- Integration tests
- CI/CD pipeline using GitHub Actions
- Health checks
- Cloud deployment

---

# Author

Tuhin Chowdhury

## License

This project was developed as part of a technical assessment.
