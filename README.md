# Comic Book Subscription API

A modern REST API for managing a comic book subscription service, built with ASP.NET Core 8.0 and Swagger documentation.

## 🚀 Features

- **Comic Book Management**: Add, update, and manage comic book catalog
- **Subscription Management**: Handle user subscriptions and billing
- **User Management**: User registration, authentication, and profiles
- **Reading Progress**: Track reading progress and bookmarks
- **Recommendations**: Personalized comic book recommendations
- **RESTful API**: Clean, intuitive API endpoints
- **Swagger Documentation**: Interactive API documentation
- **Modern Architecture**: Built with .NET 8.0 and best practices

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Language**: C#
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

### Quick Test Command

```bash
# Test the weather forecast endpoint (currently working)
curl http://localhost:5292/weatherforecast

# Test with verbose output
curl -v http://localhost:5292/weatherforecast

# Test Swagger documentation
curl http://localhost:5292/swagger
```

### Expected JSON Response

```json
[
  {
    "date": "2025-07-28",
    "temperatureC": 51,
    "summary": "Hot",
    "temperatureF": 123
  },
  {
    "date": "2025-07-29",
    "temperatureC": 43,
    "summary": "Balmy",
    "temperatureF": 109
  }
  // ... more forecast entries
]
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
│   ├── bin/                      # Build output (ignored by git)
│   ├── obj/                      # Build artifacts (ignored by git)
│   ├── Properties/               # Launch settings and configuration
│   ├── Program.cs                # Application entry point
│   ├── appsettings.json          # Configuration files
│   ├── appsettings.Development.json
│   ├── ComicBookApi.csproj       # Project file
│   └── ComicBookApi.http         # HTTP test file
├── README.md                     # This file
├── .gitignore                    # Git ignore rules
└── comic-book-api.sln           # Solution file
```

## 🔧 Configuration

The application uses `appsettings.json` for configuration. Key settings include:

- Logging configuration
- Environment-specific settings
- Allowed hosts configuration

## 🧪 Development

### Running in Development Mode

```bash
dotnet run --environment Development
```

### Building the Project

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Troubleshooting

#### Port Already in Use
If you get "address already in use" error:

```bash
# Kill processes using port 5292
sudo lsof -ti:5292 | xargs kill -9

# Or use a different port
dotnet run --urls "http://localhost:5000"
```

#### Project Not Found
Make sure you're in the correct directory:

```bash
# Navigate to the project directory
cd ComicBookApi

# Then run
dotnet run
```

## 📝 Current API Endpoints

### Working Endpoints
- `GET /weatherforecast` - Get 5-day weather forecast (example endpoint)
- `GET /swagger` - Access Swagger UI documentation
- `GET /swagger/v1/swagger.json` - Get OpenAPI specification

### Planned Endpoints

#### Comic Books
- `GET /api/comics` - Get all comic books
- `GET /api/comics/{id}` - Get comic book by ID
- `POST /api/comics` - Add new comic book
- `PUT /api/comics/{id}` - Update comic book
- `DELETE /api/comics/{id}` - Delete comic book

#### Subscriptions
- `GET /api/subscriptions` - Get user subscriptions
- `POST /api/subscriptions` - Create subscription
- `PUT /api/subscriptions/{id}` - Update subscription
- `DELETE /api/subscriptions/{id}` - Cancel subscription

#### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Register new user
- `PUT /api/users/{id}` - Update user profile

## 🎯 HTTP Test File

The project includes `ComicBookApi.http` with ready-to-use test requests:

```http
@ComicBookApi_HostAddress = http://localhost:5292

### Test Weather Forecast API
GET {{ComicBookApi_HostAddress}}/weatherforecast
Accept: application/json

### Test Swagger Documentation
GET {{ComicBookApi_HostAddress}}/swagger
Accept: text/html
```

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
- [x] Example weather forecast endpoint
- [x] Project documentation and .gitignore
- [ ] Replace weather forecast with comic book endpoints
- [ ] Database integration (Entity Framework Core)
- [ ] Authentication and authorization (JWT)
- [ ] User management endpoints
- [ ] Comic book catalog management
- [ ] Subscription and billing system
- [ ] Reading progress tracking
- [ ] Recommendation engine
- [ ] File upload for comic covers
- [ ] Search and filtering
- [ ] Rate limiting and caching
- [ ] Unit and integration tests
- [ ] Docker containerization
- [ ] CI/CD pipeline

## 🎉 Current Status

✅ **API is running and working!**
- Port: 5292
- Status: Development environment
- Weather forecast endpoint: ✅ Working
- Swagger documentation: ✅ Available
- Ready for comic book features development

---

**Happy coding! 🦸‍♂️📚**
