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
		public async Task GetAllUsers_ReturnWithAListOfUserResources()
		{
			//Arrange
			var expectedObject = GetTestUserResources();
			// setup GetAllUsers method to return test users
			_mockUserService.Setup(service => service.GetAllUsers())
				.ReturnsAsync(GetTestUsers());
			// inject mocked IUSerService and _mapper in controller
			var controller = new UsersController(_mockUserService.Object, _mapper);

			//Act
			var actionResult = await controller.GetAllUsers();
			var userResources = GetObjectResultContent<IEnumerable<UserResource>>(actionResult);

			//Assert
			Assert.IsType<ActionResult<IEnumerable<UserResource>>>(actionResult);
			Assert.Equal(serializeObject(expectedObject), serializeObject(userResources));
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
			var actionResult = await controller.GetUserById(testUserId);
			var userResource = GetObjectResultContent<UserResource>(actionResult);

			//Assert
			Assert.IsType<ActionResult<UserResource>>(actionResult);
			Assert.Equal(serializeObject(expectedUserResource), serializeObject(userResource));
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
			});
			userResources.Add(new UserResource()
			{
				Id = 2,
			});

			return userResources;
		}

		private IEnumerable<User> GetTestUsers()
		{
			var users = new List<User>();
			users.Add(new User()
			{
				Id = 1,
			});
			users.Add(new User()
			{
				Id = 2,
			});

			return users;
		}

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T)((ObjectResult)result.Result).Value;
		}
	}
}