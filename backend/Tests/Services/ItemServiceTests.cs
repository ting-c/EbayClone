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

        [Fact]
        public async Task GetItemById_ReturnItem()
        {
            // Arrange
            var item = new Item()
            {   
                Id = 1,
                Title = "Title 1",
                SellerId = 1
            };
            _mockUnitOfWork.Setup(u => u.Items.GetWithUserByIdAsync(item.Id))
                .ReturnsAsync(item);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetItemById(item.Id);

            // Assert
            Assert.Equal<Item>(item, result);
        }

        [Fact]
        public async Task GetItemsByUserId_ReturnItems()
        {
            // Arrange
            var sellerId = 1;
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
					SellerId = 1
				}
			};
            _mockUnitOfWork.Setup(u => u.Items.GetAllWithUserbyUserIdAsync(sellerId))
                .ReturnsAsync(items);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetItemsByUserId(sellerId);

            // Assert
            Assert.Equal<IEnumerable<Item>>(items, result);
        }
    }
}