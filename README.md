# AuthenticationSystem

A clean and modular **.NET 7 Web API** for user authentication using **JWT (JSON Web Tokens)**. Built using **Onion Architecture** and **Repository Pattern**, with EF Core and Swagger integrated.


## Project Structure

```
/Auth.API             -> API layer (controllers, startup)
/Auth.Application     -> Application logic, DTOs, interfaces
/Auth.Domain          -> Domain entities and contracts
/Auth.Infrastructure  -> Data access, JWT services, EF DbContext
```

## Authentication Flow

### 1. **Register**
- Endpoint: `POST /api/Auth/register`
- Body:
```json
{
  "firstName": "Temi",
  "lastName": "Sam",
  "username": "temmy02",
  "password": "testing123"
}
```
- Response:
```json
{
  "token": "JWT_TOKEN_STRING"
}
```

### 2. **Login**
- Endpoint: `POST /api/Auth/login`
- Body:
```json
{
  "username": "temmy02",
  "password": "testing123"
}
```
- Response:
```json
{
  "token": "JWT_TOKEN_STRING"
}
```

## API Testing (Swagger UI)

- Launch the API
- Navigate to: `https://localhost:{port}/swagger`
- Use **Try it out** to test `/register` and `/login`
- Copy the token returned from login
- Click **Authorize** and Enter:
```
Bearer YOUR_TOKEN_HERE
```
- Now test secured endpoints (e.g., `/api/Auth/protected`)


## How to Run

### 1. Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AuthDb;Trusted_Connection=True;"
},
"Jwt": {
  "Key": "YourSuperSecretKeyHere!"
}
```

> Replace connection string with your local SQL Server config if different.


### 2. Apply Migrations

Open **Package Manager Console** and run:

```powershell
Add-Migration InitialCreate -Project Auth.Infrastructure -StartupProject Auth.API
Update-Database -Project Auth.Infrastructure -StartupProject Auth.API
```

### 3. Run the API

```bash
dotnet run --project Auth.API
```

Visit `https://localhost:{port}/swagger` in your browser.


## Sample Users

You can register any user via the `/register` endpoint. Passwords are hashed automatically.


## Features

- JWT-based auth
- Register / Login
- Hashed passwords (custom hasher)
- Token generator service
- Swagger integration
- Clean Onion architecture
- EF Core migrations and repository pattern


## License

MIT — free to use, modify and share.
