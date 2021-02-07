using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Controllers;
using EbayClone.API.Mapping;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Tests.API.Controllers
{
    public class AuthControllerTests
    {
		private readonly Mock<IAuthService> _mockService;
		private readonly IMapper _mapper;

        public AuthControllerTests()
        {
            this._mockService = new Mock<IAuthService>();
			var mappingProfile = new MappingProfile();
			var config = new MapperConfiguration(config => config.AddProfile(mappingProfile));
			this._mapper = config.CreateMapper();
        }

		[Fact]
		public async Task SignUp_ReturnConflictObjectResult_WhenEmailIsTaken()
		{
			//Arrange
            var userSignUpResource = new UserSignUpResource();
            var expectedErrorMessage = "Email has already been taken";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync(new User());
			_mockService.Setup(service => service.FindUserByUsername(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignUp(userSignUpResource);
            var objectResult = actionResult as ConflictObjectResult;
            var errorMessage = objectResult.Value;

			//Assert
			Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedErrorMessage, errorMessage);
		}
        
		[Fact]
		public async Task SignUp_ReturnConflictObjectResult_WhenUsernameIsTaken()
		{
			//Arrange
            var userSignUpResource = new UserSignUpResource();
            var expectedErrorMessage = "Username has already been taken";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			_mockService.Setup(service => service.FindUserByUsername(It.IsAny<string>()))
				.ReturnsAsync(new User());
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignUp(userSignUpResource);
            var objectResult = actionResult as ConflictObjectResult;
            var errorMessage = objectResult.Value;

			//Assert
			Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedErrorMessage, errorMessage);
		}

		[Fact]
		public async Task SignUp_ReturnProblemObjectResult_WhenCreateNewUserFailed()
		{
			//Arrange
            var userSignUpResource = new UserSignUpResource();
            var expectedErrorMessage = "Sign up error";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			_mockService.Setup(service => service.FindUserByUsername(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			_mockService.Setup(service => service.CreateNewUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(false);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignUp(userSignUpResource);
            var objectResult = actionResult as ObjectResult;
            var errorMessage = objectResult.Value;

			//Assert
			Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal(expectedErrorMessage, errorMessage);
		}

		[Fact]
		public async Task SignUp_ReturnCreatedResult_WhenCreateNewUserIsSuccess()
		{
			//Arrange
            var userSignUpResource = new UserSignUpResource();
            var expectedValue = "Sign up success";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			_mockService.Setup(service => service.FindUserByUsername(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			_mockService.Setup(service => service.CreateNewUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(true);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignUp(userSignUpResource);
            var objectResult = actionResult as CreatedResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(expectedValue, value);
		}

		[Fact]
		public async Task SignIn_ReturnNotFoundResult_WhenUserIsNotFound()
		{
			//Arrange
            var userSignInResource = new UserSignInResource();
            var expectedValue = "User not found";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync((User)null);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignIn(userSignInResource);
            var objectResult = actionResult as NotFoundObjectResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal(expectedValue, value);
		}

		[Fact]
		public async Task SignIn_ReturnBadRequestObjectResult_WhenPasswordIsIncorrect()
		{
			//Arrange
            var userSignInResource = new UserSignInResource();
            var expectedValue = "Email or password is incorrect";
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync(new User());
			_mockService.Setup(service => service.IsUserPasswordCorrect(It.IsAny<User>(), It.IsAny<string>()))
				.ReturnsAsync(false);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignIn(userSignInResource);
            var objectResult = actionResult as BadRequestObjectResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedValue, value);
		}

		[Fact]
		public async Task SignIn_ReturnOkObjectResult_WhenSignInIsSuccess()
		{
			//Arrange
            var userSignInResource = new UserSignInResource();
            var jwtString = "MockJwt";
            var user = new User()
            {
                Id = 1,
                FirstName = "User1"
            };
            var userResource = _mapper.Map<User, UserResource>(user);
            var authResource = new AuthResource()
            {
                JwtString = jwtString,
                User = userResource
            };
			_mockService.Setup(service => service.FindUserByEmail(It.IsAny<string>()))
				.ReturnsAsync(user);
			_mockService.Setup(service => service.IsUserPasswordCorrect(It.IsAny<User>(), It.IsAny<string>()))
				.ReturnsAsync(true);
			_mockService.Setup(service => service.GetUserRoles(It.IsAny<User>()))
				.ReturnsAsync(new List<string>());
			_mockService.Setup(service => service.GenerateJwt(It.IsAny<User>(), It.IsAny<List<string>>()))
				.Returns(jwtString);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.SignIn(userSignInResource);
            var objectResult = actionResult as OkObjectResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(authResource), serializeObject(value));
		}

		[Fact]
		public async Task CreateNewRole_ReturnBadRequestResult_WhenStringIsNullOrEmpty()
		{
			//Arrange
            var roleName = "";
			var controller = new AuthController(_mapper, _mockService.Object);
            var expectedValue = "Role name must be provided";

			//Act
			var actionResult = await controller.CreateNewRole(roleName);
            var objectResult = actionResult as BadRequestObjectResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
			Assert.Equal(serializeObject(expectedValue), serializeObject(value));
		}

		[Fact]
		public async Task CreateNewRole_ReturnObjectResult_WhenCreateNewRoleFailed()
		{
			//Arrange
            var roleName = "client";
            var errorDescription = "Failed to create new role";
            var error = new IdentityError()
            {
                Description = errorDescription
            };
			_mockService.Setup(service => service.CreateNewRole(It.IsAny<string>()))
			.ReturnsAsync(IdentityResult.Failed(error));
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.CreateNewRole(roleName);
            var objectResult = actionResult as ObjectResult;
            var value = objectResult.Value;

			//Assert
			Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal(errorDescription, value);
		}

		[Fact]
		public async Task CreateNewRole_ReturnOkResult_WhenCreateNewRoleIsSuccess()
		{
			//Arrange
            var roleName = "client";
			_mockService.Setup(service => service.CreateNewRole(It.IsAny<string>()))
			.ReturnsAsync(IdentityResult.Success);
			var controller = new AuthController(_mapper, _mockService.Object);

			//Act
			var actionResult = await controller.CreateNewRole(roleName);

			//Assert
			Assert.IsType<OkResult>(actionResult);
		}

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T)((ObjectResult)result.Result).Value;
		}

		private string serializeObject<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
		
	}
}