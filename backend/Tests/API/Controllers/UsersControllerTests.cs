using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EbayClone.API.Controllers;
using EbayClone.API.Mapping;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Tests.API.Controllers
{
	public class UsersControllerTests
	{
		private readonly Mock<IUserService> _mockUserService;
		private readonly IMapper _mapper;

		public UsersControllerTests()
		{
			this._mockUserService = new Mock<IUserService>();
			// create mapper using MappingProfile in API
			var mappingProfile = new MappingProfile();
			var config = new MapperConfiguration(config =>
				config.AddProfile(mappingProfile));
			this._mapper = config.CreateMapper();
		}

		[Fact]
		public async Task GetAllUsers_ReturnWithAListOfUserResources_WhenSuccess()
		{
			//Arrange
			var expectedObject = GetTestUserResources();
			// setup GetAllUsers method to return test users
			_mockUserService.Setup(service => service.GetAllUsers())
				.ReturnsAsync(GetTestUsers());
			// inject mocked IUSerService and _mapper in controller
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.GetAllUsers() as OkObjectResult;
			var userResources = GetObjectResultContent<IEnumerable<UserResource>>(actionResult);

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(expectedObject), serializeObject(userResources));
		}

		[Fact]
		public async Task GetAllUsers_ReturnNotFound_WhenUsersAreNotFound()
		{
			//Arrange
			var expectedObject = GetTestUserResources();
			// setup GetAllUsers method to return null
			_mockUserService.Setup(service => service.GetAllUsers())
				.ReturnsAsync((IEnumerable<User>)null);
			// inject mocked IUSerService and _mapper in controller
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.GetAllUsers();

			//Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task GetUserById_ReturnUserResource_WhenUserIsFound()
		{
			//Arrange
            var testUserId = 1;
            var expectedUser = GetTestUsers().FirstOrDefault(
				u => u.Id == testUserId
			);
			var expectedUserResource = GetTestUserResources().FirstOrDefault(
                u => u.Id == testUserId
            );
			// setup GetAllUsers method to return test users
			_mockUserService.Setup(service => service.GetUserById(testUserId))
				.ReturnsAsync(expectedUser);
			// inject mocked IUSerService and _mapper in controller
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.GetUserById(testUserId) as OkObjectResult;
			var userResource = GetObjectResultContent<UserResource>(actionResult);

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(expectedUserResource), serializeObject(userResource));
		}

		[Fact]
		public async Task GetUserById_ReturnNotFoundResult_WhenUserIsNotFound()
		{
			//Arrange
            var testUserId = 3;
            // expectedUser should be null
            var expectedUser = GetTestUsers().FirstOrDefault(
				u => u.Id == testUserId
			);
			// setup GetAllUsers method to return test users
			_mockUserService.Setup(service => service.GetUserById(testUserId))
				.ReturnsAsync(expectedUser);
			// inject mocked IUSerService and _mapper in controller
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.GetUserById(testUserId);

			//Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task CreateUser_ReturnBadRequest_WhenSaveUserResourceIsInvalid()
		{
			//Arrange
            var saveUserResource = new SaveUserResource()
            {
                Name = null
            };
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.CreateUser(saveUserResource);

			//Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
		}

		[Fact]
		public async Task CreateUser_ReturnNotFoundResult_WhenNewUserIsNull()
		{
			//Arrange
			var saveUserResource = new SaveUserResource()
			{
				Name = "User 3"
			};
			var userToCreate = new User()
			{
				Id = 3,
				Name = "User 3",
			};
			// setup GetAllUsers method to return null
			_mockUserService.Setup(service => service.CreateUser(userToCreate))
				.ReturnsAsync((User)null);
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.CreateUser(saveUserResource);

			//Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		private string serializeObject<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		private IEnumerable<UserResource> GetTestUserResources()
		{
			var userResources = new List<UserResource>();
			userResources.Add(new UserResource()
			{
				Id = 1,
                Name = "User 1"
			});
			userResources.Add(new UserResource()
			{
				Id = 2,
                Name = "User 2"
			});

			return userResources;
		}

		private IEnumerable<User> GetTestUsers()
		{
			var users = new List<User>();
			users.Add(new User()
			{
				Id = 1,
                Name = "User 1",
				SellingItems = new List<Item>()
				{
					new Item(){ Id = 1, Title = "Title 1", SellerId = 1 },
					new Item(){ Id = 2, Title = "Title 2", SellerId = 1 }
				}
			});
			users.Add(new User()
			{
				Id = 2,
                Name = "User 2",
                SellingItems = new List<Item>()
                {
                    new Item(){ Id = 3, Title = "Title 3", SellerId = 2 },    
                    new Item(){ Id = 4, Title = "Title 4", SellerId = 2 }    
                }
			});

			return users;
		}

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T)((ObjectResult)result.Result).Value;
		}
	}
}