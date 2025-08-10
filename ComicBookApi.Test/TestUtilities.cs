using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using ComicBookApi.Models;
using System.Security.Claims;
using ComicBookApi.Controllers;

namespace ComicBookApi.Test
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
    }
} 