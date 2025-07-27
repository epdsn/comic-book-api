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

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- **API**: https://localhost:7001
- **Swagger UI**: https://localhost:7001/swagger

## 📚 API Documentation

Once the application is running, you can access the interactive Swagger documentation at:
```
https://localhost:7001/swagger
```

This provides a complete overview of all available endpoints, request/response schemas, and allows you to test the API directly from the browser.

## 🏗️ Project Structure

```
comic-book-api/
├── ComicBookApi/                 # Main API project
│   ├── Controllers/              # API controllers
│   ├── Models/                   # Data models and DTOs
│   ├── Services/                 # Business logic services
│   ├── Data/                     # Data access layer
│   ├── Program.cs                # Application entry point
│   └── appsettings.json          # Configuration files
├── README.md                     # This file
└── comic-book-api.sln           # Solution file
```

## 🔧 Configuration

The application uses `appsettings.json` for configuration. Key settings include:

- Database connection strings
- API authentication settings
- Logging configuration
- Environment-specific settings

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

## 📝 API Endpoints (Planned)

### Comic Books
- `GET /api/comics` - Get all comic books
- `GET /api/comics/{id}` - Get comic book by ID
- `POST /api/comics` - Add new comic book
- `PUT /api/comics/{id}` - Update comic book
- `DELETE /api/comics/{id}` - Delete comic book

### Subscriptions
- `GET /api/subscriptions` - Get user subscriptions
- `POST /api/subscriptions` - Create subscription
- `PUT /api/subscriptions/{id}` - Update subscription
- `DELETE /api/subscriptions/{id}` - Cancel subscription

### Users
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Register new user
- `PUT /api/users/{id}` - Update user profile

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

---

**Happy coding! 🦸‍♂️📚**
