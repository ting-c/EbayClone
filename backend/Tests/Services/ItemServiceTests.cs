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
    public class ItemServiceTests
    {
		private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly List<Item> _items;

        public ItemServiceTests()
        {
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._items = new List<Item>()
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
        }

        [Fact]
        public async Task GetAllWithUser_ReturnAllItems()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Items.GetAllWithUserAsync())
                .ReturnsAsync(_items);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetAllWithUser();

            // Assert
            Assert.Equal<IEnumerable<Item>>(_items, result);
        }

        [Fact]
        public async Task GetItemById_ReturnItem()
        {
            // Arrange
            var testItemId = 1;
            var expectedItem = _items.FirstOrDefault(
                i => i.Id == testItemId
            );
            _mockUnitOfWork.Setup(u => u.Items.GetWithUserByIdAsync(testItemId))
                .ReturnsAsync(expectedItem);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetItemById(testItemId);

            // Assert
            Assert.Equal<Item>(expectedItem, result);
        }

        [Fact]
        public async Task GetItemsByUserId_ReturnItems()
        {
            // Arrange
            var testSellerId = 1;
            var expectedItems = _items.FindAll(
                i => i.SellerId == testSellerId
            );
            _mockUnitOfWork.Setup(u => u.Items.GetAllWithUserbyUserIdAsync(testSellerId))
                .ReturnsAsync(expectedItems);
            var itemService = new ItemService(_mockUnitOfWork.Object);

            // Act
            var result = await itemService.GetItemsByUserId(testSellerId);

            // Assert
            Assert.Equal<IEnumerable<Item>>(expectedItems, result);
        }
    }
}