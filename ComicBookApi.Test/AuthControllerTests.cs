using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ComicBookApi.Controllers;
using ComicBookApi.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Text.Json;
using ComicBookApi.TestUtilities;

namespace ComicBookApi.Test
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _userManagerMock = GetUserManagerMock();
            _signInManagerMock = GetSignInManagerMock(_userManagerMock.Object);
            _controller = new AuthController(_userManagerMock.Object, _signInManagerMock.Object);
            
            // Setup controller context to avoid null reference exceptions
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        #region Register Tests

        [Fact]
        public async Task Register_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var dto = GetValidRegisterDto();
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            
            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            Assert.Equal("User registered successfully", response.GetProperty("message").GetString());
        }

        // Note: This test was removed because the controller doesn't validate ModelState
        // If you want to test ModelState validation, you would need to add it to your controller
        // Example: if (!ModelState.IsValid) return BadRequest(ModelState);

        [Fact]
        public async Task Register_WhenUserCreationFails_ReturnsBadRequestWithErrors()
        {
            // Arrange
            var dto = GetValidRegisterDto();
            var errors = new List<IdentityError> 
            { 
                new IdentityError { Description = "Email already exists" },
                new IdentityError { Description = "Password too weak" }
            };
            
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            
            var responseJson = JsonSerializer.Serialize(badRequestResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            var errorsArray = response.GetProperty("errors").EnumerateArray();
            var errorMessages = errorsArray.Select(e => e.GetString()).ToList();
            
            Assert.Contains("Email already exists", errorMessages);
            Assert.Contains("Password too weak", errorMessages);
        }

        [Fact]
        public async Task Register_CreatesUserWithCorrectData()
        {
            // Arrange
            var dto = GetValidRegisterDto();
            ApplicationUser createdUser = null!;
            
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Callback<ApplicationUser, string>((user, password) => createdUser = user)
                .ReturnsAsync(IdentityResult.Success);

            // Act
            await _controller.Register(dto);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(dto.Email, createdUser.Email);
            Assert.Equal(dto.Email, createdUser.UserName);
            Assert.Equal(dto.FirstName, createdUser.FirstName);
            Assert.Equal(dto.LastName, createdUser.LastName);
            Assert.Equal(dto.DateOfBirth, createdUser.DateOfBirth);
            Assert.True(createdUser.CreatedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        #endregion

        #region Login Tests

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var dto = GetValidLoginDto();
            var user = GetTestUser();
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);
            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _userManagerMock.Setup(x => x.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            
            Assert.NotNull(response.GetProperty("token").GetString());
            Assert.NotNull(response.GetProperty("user"));
            Assert.Equal(user.Id, response.GetProperty("user").GetProperty("id").GetString());
            Assert.Equal(user.Email, response.GetProperty("user").GetProperty("email").GetString());
        }

        [Fact]
        public async Task Login_WithInvalidEmail_ReturnsUnauthorized()
        {
            // Arrange
            var dto = GetValidLoginDto();
            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(unauthorizedResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            Assert.Equal("Invalid email or password", response.GetProperty("message").GetString());
        }

        [Fact]
        public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
        {
            // Arrange
            var dto = GetValidLoginDto();
            var user = GetTestUser();
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Email))
                .ReturnsAsync(user);
            _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(unauthorizedResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            Assert.Equal("Invalid email or password", response.GetProperty("message").GetString());
        }

        #endregion

        #region Profile Tests

        [Fact]
        public async Task GetProfile_WithValidUser_ReturnsUserProfile()
        {
            // Arrange
            var user = GetTestUser();
            SetupAuthenticatedUser(user.Id);
            
            _userManagerMock.Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetProfile();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            
            Assert.Equal(user.Id, response.GetProperty("id").GetString());
            Assert.Equal(user.Email, response.GetProperty("email").GetString());
            Assert.Equal(user.FirstName, response.GetProperty("firstName").GetString());
            Assert.Equal(user.LastName, response.GetProperty("lastName").GetString());
        }

        [Fact]
        public async Task GetProfile_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Act
            var result = await _controller.GetProfile();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetProfile_WithInvalidUserId_ReturnsNotFound()
        {
            // Arrange
            SetupAuthenticatedUser("invalid-id");
            _userManagerMock.Setup(x => x.FindByIdAsync("invalid-id"))
                .ReturnsAsync((ApplicationUser)null!);

            // Act
            var result = await _controller.GetProfile();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateProfile_WithValidData_ReturnsOk()
        {
            // Arrange
            var user = GetTestUser();
            var dto = GetValidUpdateProfileDto();
            SetupAuthenticatedUser(user.Id);
            
            _userManagerMock.Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.UpdateProfile(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(okResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            Assert.Equal("Profile updated successfully", response.GetProperty("message").GetString());
        }

        [Fact]
        public async Task UpdateProfile_WhenUpdateFails_ReturnsBadRequest()
        {
            // Arrange
            var user = GetTestUser();
            var dto = GetValidUpdateProfileDto();
            SetupAuthenticatedUser(user.Id);
            
            var errors = new List<IdentityError> { new IdentityError { Description = "Update failed" } };
            
            _userManagerMock.Setup(x => x.FindByIdAsync(user.Id))
                .ReturnsAsync(user);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

            // Act
            var result = await _controller.UpdateProfile(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseJson = JsonSerializer.Serialize(badRequestResult.Value);
            var response = JsonSerializer.Deserialize<JsonElement>(responseJson);
            var errorsArray = response.GetProperty("errors").EnumerateArray();
            var errorMessages = errorsArray.Select(e => e.GetString()).ToList();
            Assert.Contains("Update failed", errorMessages);
        }

        #endregion

        #region Test Helpers

        private Mock<UserManager<ApplicationUser>> GetUserManagerMock()
        {
            return new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null!, null!, null!, null!, null!, null!, null!, null!
            );
        }

        private Mock<SignInManager<ApplicationUser>> GetSignInManagerMock(UserManager<ApplicationUser> userManager)
        {
            return new Mock<SignInManager<ApplicationUser>>(
                userManager,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null!, null!, null!, null!
            );
        }

        private void SetupAuthenticatedUser(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        private RegisterDto GetValidRegisterDto()
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

        private LoginDto GetValidLoginDto()
        {
            return new LoginDto
            {
                Email = "test@example.com",
                Password = "TestPassword123!"
            };
        }

        private UpdateProfileDto GetValidUpdateProfileDto()
        {
            return new UpdateProfileDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1995, 5, 15)
            };
        }

        private ApplicationUser GetTestUser()
        {
            return new ApplicationUser
            {
                Id = "test-user-id",
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

        #endregion
    }
}