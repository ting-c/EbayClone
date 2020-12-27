using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Services;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class UserServiceTests
    {
		private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly List<User> _users;

        public UserServiceTests()
        {
		    this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "User 1"
                },
				new User()
				{
					Id = 2,
					FirstName = "User 2"
				}
            };
        }

		[Fact]
		public async Task GetAllUsers_ReturnAllUsers()
		{
			// Arrange
			_mockUnitOfWork.Setup(u => u.Users.GetAllAsync())
				.ReturnsAsync(_users);
			var userService = new UserService(_mockUnitOfWork.Object);

			// Act
			var result = await userService.GetAllUsers();

			// Assert
			Assert.Equal<IEnumerable<User>>(_users, result);
		}

		[Fact]
		public async Task GetUserById_ReturnUser()
		{
			// Arrange
            var testUserId = 2;
			var expectedUser = _users.FirstOrDefault(
                u => u.Id == testUserId
            );
			_mockUnitOfWork.Setup(u => u.Users.GetByIdAsync(testUserId))
				.ReturnsAsync(expectedUser);
			var userService = new UserService(_mockUnitOfWork.Object);

			// Act
			var result = await userService.GetUserById(testUserId);

			// Assert
			Assert.Equal<User>(expectedUser, result);
		}
    }
}