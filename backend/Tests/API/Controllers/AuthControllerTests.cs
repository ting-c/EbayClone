using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Controllers;
using EbayClone.API.Mapping;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T)((ObjectResult)result.Result).Value;
		}
		
	}
}