using System.Collections.Generic;
using System.Threading.Tasks;
using EbayClone.Core;
using EbayClone.Core.Models;
using EbayClone.Services;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class ItemServiceTests
    {
		private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ItemServiceTests()
        {
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task GetAllWithUser_ReturnAllItems()
        {
            // Arrange
            var items = new List<Item>()
            {
                new Item()
                {
                    Title = "Title 1",
                    SellerId = 1
                },
                new Item()
                {
                    Title = "Title 2",
                    SellerId = 2
                }
            };
            _mockUnitOfWork.Setup(u => u.Items.GetAllWithUserAsync())
                .ReturnsAsync(items);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetAllWithUser();

            // Assert
            Assert.Equal<IEnumerable<Item>>(items, result);
        }
    }
}