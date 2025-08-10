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


public class AuthControllerTests
{
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


    [Fact]
    public async Task Register_ReturnsOk_WhenUserIsCreated()
    {
        // Arrange
        var userManagerMock = GetUserManagerMock();
        var signInManagerMock = GetSignInManagerMock(userManagerMock.Object);

        userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var controller = new AuthController(userManagerMock.Object, signInManagerMock.Object);

        var dto = new RegisterDto
        {
            Email = "epdevio@gmail.com",
            Password = "Password1234!",
            FirstName = "Eric",
            LastName = "Perez",
            DateOfBirth = new DateTime(2000, 1, 1)
        };

        // Act
        var result = await controller.Register(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenUserCreationFails()
    {
        // Arrange
        var userManagerMock = GetUserManagerMock();
        var signInManagerMock = GetSignInManagerMock(userManagerMock.Object);

        // Simulate a failed user creation with an error message
        var errors = new List<IdentityError> { new IdentityError { Description = "Error" } };
        userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(errors.ToArray()));

        var controller = new AuthController(userManagerMock.Object, signInManagerMock.Object);

        var dto = new RegisterDto
        {
            Email = "fail@example.com",
            Password = "Password123!",
            FirstName = "Fail",
            LastName = "User",
            DateOfBirth = new DateTime(2000, 1, 1)
        };

        // Act
        var result = await controller.Register(dto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

}