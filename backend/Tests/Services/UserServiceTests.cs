using System.Collections.Generic;
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

        public UserServiceTests()
        {
		    this._mockUnitOfWork = new Mock<IUnitOfWork>();
        }

		[Fact]
		public async Task GetAllUsers_ReturnAllUsers()
		{
			// Arrange
			var users = new List<User>()
			{
				new User()
				{
					Id = 1,
					Name = "User 1"
				},
				new User()
				{
					Id = 2,
					Name = "User 2"
				}
			};
			_mockUnitOfWork.Setup(u => u.Users.GetAllAsync())
				.ReturnsAsync(users);
			var userService = new UserService(_mockUnitOfWork.Object);

			// Act
			var result = await userService.GetAllUsers();

			// Assert
			Assert.Equal<IEnumerable<User>>(users, result);
		}
    }
}