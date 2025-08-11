using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using ComicBookApi.Models;
using System.Security.Claims;
using ComicBookApi.Controllers;

namespace ComicBookApi.TestUtilities
{
    public static class TestUtilities
    {
        public static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
        {
            return new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null!, null!, null!, null!, null!, null!, null!, null!
            );
        }

        public static Mock<SignInManager<ApplicationUser>> CreateSignInManagerMock(UserManager<ApplicationUser> userManager)
        {
            return new Mock<SignInManager<ApplicationUser>>(
                userManager,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null!, null!, null!, null!
            );
        }

        public static void SetupAuthenticatedUser(ControllerBase controller, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        public static ApplicationUser CreateTestUser(string id = "test-user-id")
        {
            return new ApplicationUser
            {
                Id = id,
                Email = "test@example.com",
                UserName = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                IsPremium = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                LastLoginAt = DateTime.UtcNow.AddHours(-2)
            };
        }

        public static List<IdentityError> CreateIdentityErrors(params string[] errorMessages)
        {
            return errorMessages.Select(msg => new IdentityError { Description = msg }).ToList();
        }
    }

    public static class TestDataBuilder
    {
        public static RegisterDto CreateValidRegisterDto()
        {
            return new RegisterDto
            {
                Email = "test@example.com",
                Password = "TestPassword123!",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
        }

        public static RegisterDto CreateInvalidRegisterDto()
        {
            return new RegisterDto
            {
                Email = "invalid-email",
                Password = "weak",
                FirstName = "",
                LastName = "",
                DateOfBirth = DateTime.UtcNow.AddYears(1) // Future date
            };
        }

        public static LoginDto CreateValidLoginDto()
        {
            return new LoginDto
            {
                Email = "test@example.com",
                Password = "TestPassword123!"
            };
        }

        public static LoginDto CreateInvalidLoginDto()
        {
            return new LoginDto
            {
                Email = "invalid-email",
                Password = ""
            };
        }

        public static UpdateProfileDto CreateValidUpdateProfileDto()
        {
            return new UpdateProfileDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1995, 5, 15)
            };
        }

        public static UpdateProfileDto CreateInvalidUpdateProfileDto()
        {
            return new UpdateProfileDto
            {
                FirstName = "",
                LastName = "",
                DateOfBirth = DateTime.UtcNow.AddYears(1) // Future date
            };
        }

        public static List<ComicBook> GetSampleComicBooks()
        {
            return new List<ComicBook>
            {
                new ComicBook
                {
                    Id = 1,
                    Title = "The Awakening",
                    Series = "Dark Lobo",
                    Issue = "#1",
                    CoverImage = "https://via.placeholder.com/300x400/1a1a2e/ffffff?text=Dark+Lobo+1",
                    Description = "In a world where darkness reigns, one hero emerges to challenge the shadows.",
                    Genre = "Superhero",
                    Rating = 4.5m,
                    Price = 3.99m,
                    ReleaseDate = new DateTime(2024, 1, 15)
                },
                new ComicBook
                {
                    Id = 2,
                    Title = "Shadow Hunters",
                    Series = "Shadow Hunters",
                    Issue = "#1",
                    CoverImage = "https://via.placeholder.com/300x400/16213e/ffffff?text=Shadow+Hunters+1",
                    Description = "A team of elite hunters track down supernatural threats in the modern world.",
                    Genre = "Horror",
                    Rating = 4.2m,
                    Price = 2.99m,
                    ReleaseDate = new DateTime(2024, 2, 1)
                },
                new ComicBook
                {
                    Id = 3,
                    Title = "Cyber Knights",
                    Series = "Cyber Knights",
                    Issue = "#1",
                    CoverImage = "https://via.placeholder.com/300x400/0f3460/ffffff?text=Cyber+Knights+1",
                    Description = "Futuristic warriors battle in a neon-lit cyberpunk world.",
                    Genre = "Sci-Fi",
                    Rating = 4.7m,
                    Price = 4.99m,
                    ReleaseDate = new DateTime(2024, 1, 20)
                },
                new ComicBook
                {
                    Id = 4,
                    Title = "Mystic Realms",
                    Series = "Mystic Realms",
                    Issue = "#1",
                    CoverImage = "https://via.placeholder.com/300x400/533483/ffffff?text=Mystic+Realms+1",
                    Description = "Magical adventures in a world where fantasy meets reality.",
                    Genre = "Fantasy",
                    Rating = 4.3m,
                    Price = 3.49m,
                    ReleaseDate = new DateTime(2024, 2, 10)
                },
                new ComicBook
                {
                    Id = 5,
                    Title = "Urban Legends",
                    Series = "Urban Legends",
                    Issue = "#1",
                    CoverImage = "https://via.placeholder.com/300x400/2d3748/ffffff?text=Urban+Legends+1",
                    Description = "Modern myths and legends come to life in the city streets.",
                    Genre = "Mystery",
                    Rating = 4.1m,
                    Price = 2.99m,
                    ReleaseDate = new DateTime(2024, 1, 25)
                },
                new ComicBook
                {
                    Id = 6,
                    Title = "The Last Stand",
                    Series = "Dark Lobo",
                    Issue = "#2",
                    CoverImage = "https://via.placeholder.com/300x400/1a1a2e/ffffff?text=Dark+Lobo+2",
                    Description = "The battle intensifies as our hero faces his greatest challenge yet.",
                    Genre = "Superhero",
                    Rating = 4.6m,
                    Price = 3.99m,
                    ReleaseDate = new DateTime(2024, 2, 15)
                }
            };
        }
    }
} 