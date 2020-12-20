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
    public class ItemsControllerTests
    {
        private readonly Mock<IItemService> _mockItemService;
        private readonly IMapper _mapper;

        public ItemsControllerTests()
        {
            this._mockItemService = new Mock<IItemService>();
            // create mapper using MappingProfile in API
			var mappingProfile = new MappingProfile();
			var config = new MapperConfiguration(config => 
                config.AddProfile(mappingProfile));
            this._mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllItems_ReturnWithAListOfItemResources()
        {
            //Arrange
            var expectedObject = GetTestItemResources();
            // setup GetAllWithUser method to return test items
            _mockItemService.Setup(service => service.GetAllWithUser())
                .ReturnsAsync(GetTestItems());
            // inject mocked IItemService and _mapper in controller
            var controller = new ItemsController(_mockItemService.Object, _mapper);
            
            //Act
            var actionResult = await controller.GetAllItems();
            var resultObject = GetObjectResultContent<IEnumerable<ItemResource>>(actionResult);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<ItemResource>>>(actionResult);
            Assert.Equal(serializeObject(expectedObject), serializeObject(resultObject));
        }

        [Fact]
        public async Task GetItemById_ReturnWithExpectedItemResource()
        {
			// Arrange
            var testItemId = 1;
            var itemResources = GetTestItemResources();
			_mockItemService.Setup(service => service.GetItemById(testItemId))
				.ReturnsAsync(GetTestItems().FirstOrDefault(
                    i => i.Id == testItemId));
            var controller = new ItemsController(_mockItemService.Object, _mapper);

            // Act
            var actionResult = await controller.GetItemById(testItemId);
            var resultObject = GetObjectResultContent<ItemResource>(actionResult);

			// Assert
			Assert.IsType<ActionResult<ItemResource>>(actionResult);
			Assert.Equal(1, resultObject.Id);
			Assert.Equal("Test 1", resultObject.Title);
        }

        [Fact]
        public async Task CreateItem_ReturnBadRequestResult_WhenSaveItemResourceIsInvalid()
        {
            //Arrange
            var saveItemResource = new SaveItemResource()
            {
                Title = null
            };
			var controller = new ItemsController(_mockItemService.Object, _mapper);
            
			// Act
			var actionResult = await controller.CreateItem(saveItemResource);

			// Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
		}

		[Fact]
		public async Task CreateItem_ReturnNotFoundObject_WhenCreateItemMethodReturnsNull()
		{
			//Arrange
			var itemToCreate = GetTestItems().FirstOrDefault(
				i => i.Id == 1);
			var saveItemResource = new SaveItemResource()
			{
				Title = "Test 1",
				SellerId = 1
			};

			_mockItemService.Setup(service => service.CreateItem(itemToCreate))
				.ReturnsAsync((Item)null);

			var controller = new ItemsController(_mockItemService.Object, _mapper);

			// Act
			var actionResult = await controller.CreateItem(saveItemResource);

			// Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}
        
        private string serializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private IEnumerable<ItemResource> GetTestItemResources()
        {
            var itemResources = new List<ItemResource>();
			itemResources.Add(new ItemResource()
            {
                Id = 1,
                Title = "Test 1",
                Seller = new UserResource()
            });
			itemResources.Add(new ItemResource()
            {
                Id = 2,
                Title = "Test 2",
                Seller = new UserResource()
            });

            return itemResources;
        }

        private IEnumerable<Item> GetTestItems()
        {
            var items = new List<Item>();
			items.Add(new Item()
            {
                Id = 1,
                Title = "Test 1",
                Seller = new User()
            });
			items.Add(new Item()
            {
                Id = 2,
                Title = "Test 2",
                Seller = new User()
            });

            return items;
        }

        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T) ((ObjectResult) result.Result).Value;
        } 
    }
}