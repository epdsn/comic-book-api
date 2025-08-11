# Comic Book API

A .NET 8 Web API for managing comic books with JWT authentication and user management, built with ASP.NET Core 8.0 and Swagger documentation.

## 🚀 Features

- **JWT Authentication**: Secure user authentication with JWT tokens
- **User Management**: User registration, login, and profile management
- **Comic Book Management**: Full CRUD operations for comic books
- **Repository Pattern**: Clean separation of concerns with interfaces
- **Test-Driven Development**: Comprehensive test coverage with xUnit and Moq
- **CORS Support**: Configured for Angular frontend integration
- **Swagger Documentation**: Interactive API documentation
- **Modern Architecture**: Built with .NET 8.0 and best practices
- **In-Memory Storage**: Development-ready with in-memory collections

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C#
- **Authentication**: JWT Bearer tokens
- **Testing**: xUnit with Moq for mocking
- **Architecture**: Repository pattern with dependency injection
- **Documentation**: Swagger/OpenAPI
- **Development**: Visual Studio 2022 / VS Code

## 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Git

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/comic-book-api.git
cd comic-book-api
```

### 2. Navigate to the API Project

```bash
cd ComicBookApi
```

### 3. Build the Project

```bash
dotnet build
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- **API**: http://localhost:5292
- **Swagger UI**: http://localhost:5292/swagger

## 🧪 Testing the API

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with verbose output
dotnet test --verbosity normal

# Run specific test project
dotnet test ComicBookApi.Test
```

### Quick Test Commands

```bash
# Test the auth API connection
curl http://localhost:5292/api/auth/test

# Test user registration
curl -X POST http://localhost:5292/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"TestPassword123!","firstName":"John","lastName":"Doe","dateOfBirth":"1990-01-01T00:00:00Z"}'

# Test user login
curl -X POST http://localhost:5292/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"TestPassword123!"}'

# Test comic books API
curl http://localhost:5292/api/comicbooks

# Test Swagger documentation
curl http://localhost:5292/swagger
```

### Expected API Responses

**Registration Success:**
```json
{
  "message": "User registered successfully"
}
```

**Login Success:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": "user-id",
    "email": "test@example.com",
    "firstName": "John",
    "lastName": "Doe",
    "isPremium": false
  }
}
```

## 📚 API Documentation

Once the application is running, you can access the interactive Swagger documentation at:
```
http://localhost:5292/swagger
```

This provides a complete overview of all available endpoints, request/response schemas, and allows you to test the API directly from the browser.

## 🏗️ Project Structure

```
comic-book-api/
├── ComicBookApi/                 # Main API project
│   ├── Controllers/              # API controllers
│   │   ├── AuthController.cs     # Authentication endpoints
│   │   └── ComicBooksController.cs # Comic book CRUD endpoints
│   ├── Models/                   # Data models
│   │   ├── ApplicationUser.cs    # User model with Identity
│   │   └── ComicBook.cs          # Comic book data model
│   ├── Repositories/             # Data access layer
│   │   ├── IComicBookRepository.cs # Repository interface
│   │   └── ComicBookRepository.cs  # Repository implementation
│   ├── Data/                     # Database context
│   │   └── ApplicationDbContext.cs
│   ├── Properties/               # Launch settings and configuration
│   ├── Program.cs                # Application entry point
│   ├── appsettings.json          # Configuration files
│   ├── ComicBookApi.csproj       # Project file
│   └── ComicBookApi.http         # HTTP test file
├── ComicBookApi.Test/            # Test project
│   ├── AuthControllerTests.cs    # Authentication tests
│   ├── ComicBooksControllerTests.cs # Comic book tests
│   ├── TestUtilities.cs          # Test data and utilities
│   └── ComicBookApi.Test.csproj  # Test project file
├── README.md                     # This file
├── .gitignore                    # Git ignore rules
└── comic-book-api.sln           # Solution file
```

## 🔧 Configuration

The application uses `appsettings.json` for configuration. Key settings include:

- JWT token configuration
- CORS settings for frontend integration
- Logging configuration
- Environment-specific settings

## 📝 Current API Endpoints

### Authentication Endpoints
- `GET /api/auth/test` - Test API connection
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user (returns JWT token)
- `GET /api/auth/profile` - Get user profile (requires authentication)
- `PUT /api/auth/profile` - Update user profile (requires authentication)

### Comic Book Data Model
```json
{
  "id": 1,
  "title": "The Awakening",
  "series": "Dark Lobo",
  "issue": "#1",
  "coverImage": "https://via.placeholder.com/300x400/1a1a2e/ffffff?text=Dark+Lobo+1",
  "description": "In a world where darkness reigns, one hero emerges to challenge the shadows.",
  "genre": "Superhero",
  "rating": 4.5,
  "price": 3.99,
  "releaseDate": "2024-01-15T00:00:00Z"
}
```

### Documentation Endpoints
- `GET /swagger` - Access Swagger UI documentation
- `GET /swagger/v1/swagger.json` - Get OpenAPI specification

### Comic Book Endpoints
- `GET /api/comicbooks` - Get all comic books
- `GET /api/comicbooks/{id}` - Get comic book by ID
- `POST /api/comicbooks` - Add new comic book
- `PUT /api/comicbooks/{id}` - Update comic book
- `DELETE /api/comicbooks/{id}` - Delete comic book

### Planned Endpoints

#### Subscriptions
- `GET /api/subscriptions` - Get user subscriptions
- `POST /api/subscriptions` - Create subscription
- `PUT /api/subscriptions/{id}` - Update subscription
- `DELETE /api/subscriptions/{id}` - Cancel subscription

## 🎯 HTTP Test File

The project includes `ComicBookApi.http` with ready-to-use test requests for all authentication endpoints.

## 🏛️ Architecture

### Repository Pattern
The application follows the Repository pattern for clean separation of concerns:

- **Interfaces**: Define contracts for data access (`IComicBookRepository`)
- **Implementations**: Concrete implementations of data access logic
- **Dependency Injection**: Controllers depend on interfaces, not concrete classes
- **Testability**: Easy to mock dependencies for unit testing

### Test-Driven Development
The project follows TDD principles:

- **Red-Green-Refactor**: Write failing tests first, implement minimal code, then refactor
- **Comprehensive Coverage**: 18 tests covering all CRUD operations
- **Mocking**: Uses Moq for dependency mocking
- **Test Utilities**: Reusable test data and helper methods

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication:

1. **Register** a new user to create an account
2. **Login** to receive a JWT token
3. **Include the token** in the Authorization header for protected endpoints:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

## 🌐 Frontend Integration

The API is configured with CORS to work with Angular frontends:
- Supports `localhost:4200` and `localhost:4201`
- Handles both HTTP and HTTPS
- Allows credentials and custom headers

## 🧪 Development

### Running in Development Mode

```bash
dotnet run --environment Development
```

### Building the Project

```bash
dotnet build
```

### Testing with HTTP File

Use the provided `ComicBookApi.http` file in VS Code or your preferred HTTP client to test all endpoints.

### Troubleshooting

#### Port Already in Use
If you get "address already in use" error:

```bash
# Kill processes using port 5292
sudo lsof -ti:5292 | xargs kill -9

# Or use a different port
dotnet run --urls "http://localhost:5000"
```

#### Authentication Issues
- Ensure JWT token is included in Authorization header
- Check token expiration (24 hours by default)
- Verify email/password for login

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/yourusername/comic-book-api/issues) page
2. Create a new issue with detailed information
3. Contact the development team

## 🔮 Roadmap

- [x] Basic API setup with ASP.NET Core 8.0
- [x] Swagger documentation integration
- [x] JWT authentication system
- [x] User registration and login
- [x] Profile management
- [x] CORS configuration for Angular
- [x] Comic book catalog management
- [x] Repository pattern implementation
- [x] Test-driven development setup
- [x] Comprehensive test coverage
- [ ] Subscription and billing system
- [ ] Reading progress tracking
- [ ] Recommendation engine
- [ ] File upload for comic covers
- [ ] Search and filtering
- [ ] Rate limiting and caching
- [ ] Database integration (Entity Framework)
- [ ] Docker containerization
- [ ] CI/CD pipeline

## 🎉 Current Status

✅ **API is running and working!**
- Port: 5292
- Status: Development environment
- Authentication: ✅ JWT implemented
- User management: ✅ Working
- Comic book management: ✅ Full CRUD implemented
- Repository pattern: ✅ Clean architecture
- Test coverage: ✅ 18 tests passing
- Swagger documentation: ✅ Available
- Angular integration: ✅ CORS configured
- Ready for additional features development

---

**Happy coding! 🦸‍♂️📚**
