using System.Security.Claims;
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
using Microsoft.AspNetCore.Http;
using System;

namespace Tests.API.Controllers
{
    public class ItemsControllerTests
    {
        private readonly Mock<IItemService> _mockItemService;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _user;

        public ItemsControllerTests()
        {
            this._mockItemService = new Mock<IItemService>();
            // create mapper using MappingProfile in API
			var mappingProfile = new MappingProfile();
			var config = new MapperConfiguration(config => 
                config.AddProfile(mappingProfile));
            this._mapper = config.CreateMapper();
			this._user = new ClaimsPrincipal(new ClaimsIdentity(
				 new Claim[]
				 {
				    new Claim(ClaimTypes.NameIdentifier, "1")
				 }
				 , "TestAuthentication"));
        }

        [Fact]
        public async Task GetAllItems_ReturnWithAListOfItemResources_WhenSuccess()
        {
            //Arrange
            var expectedObject = GetTestItemResources();
            // setup GetAllWithUser method to return test items
            _mockItemService.Setup(service => service.GetAllWithUser())
                .ReturnsAsync(GetTestItems());
            // inject mocked IItemService and _mapper in controller
            var controller = new ItemsController(_mockItemService.Object, _mapper);
            
            //Act
            var actionResult = await controller.GetAllItems() as OkObjectResult;
            var resultObject = GetObjectResultContent<IEnumerable<ItemResource>>(actionResult);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(serializeObject(expectedObject), serializeObject(resultObject));
        }

        [Fact]
        public async Task GetAllItems_ReturnNotFoundResult_WhenItemsAreNotFound()
        {
            //Arrange
            // setup GetAllWithUser method to return null
            _mockItemService.Setup(service => service.GetAllWithUser())
                .ReturnsAsync((IEnumerable<Item>)null);
            // inject mocked IItemService and _mapper in controller
            var controller = new ItemsController(_mockItemService.Object, _mapper);
            
            //Act
            var actionResult = await controller.GetAllItems();

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task GetItemById_ReturnWithExpectedItemResource_WhenSuccess()
        {
			// Arrange
            var testItemId = 1;
            var expectedItemResource = GetTestItemResources().FirstOrDefault(
                i => i.Id == testItemId
            );
            var itemResources = GetTestItemResources();
			_mockItemService.Setup(service => service.GetItemById(testItemId))
				.ReturnsAsync(GetTestItems().FirstOrDefault(
                    i => i.Id == testItemId));
            var controller = new ItemsController(_mockItemService.Object, _mapper);

            // Act
            var actionResult = await controller.GetItemById(testItemId) as OkObjectResult;
            var itemResource = GetObjectResultContent<ItemResource>(actionResult);

			// Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(expectedItemResource), serializeObject(itemResource));
        }

        [Fact]
        public async Task GetItemById_ReturnNotFoundResult_WhenItemIsNull()
        {
			// Arrange
            var testItemId = 3;
            var itemResources = GetTestItemResources();
            // setup GetItemById to return null
			_mockItemService.Setup(service => service.GetItemById(testItemId))
				.ReturnsAsync((Item)null);
            var controller = new ItemsController(_mockItemService.Object, _mapper);

            // Act
            var actionResult = await controller.GetItemById(testItemId);

			// Assert
			Assert.IsType<NotFoundResult>(actionResult);
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
            SetupHttpContextUser(controller, _user);
            
			// Act
			var actionResult = await controller.CreateItem(saveItemResource);

			// Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
		}

		[Fact]
		public async Task CreateItem_ReturnNotFoundObject_WhenCreateItemMethodReturnsNull()
		{
			//Arrange
			var saveItemResource = new SaveItemResource()
			{
				Title = "Item 1",
                Description = "Description",
				Price = 30.00M,
				Condition = "New",
				IsAuction = false,
				SellerId = 1
			};
            var itemToCreate = _mapper.Map<SaveItemResource, Item>(saveItemResource);

			_mockItemService.Setup(service => service.CreateItem(itemToCreate))
				.ReturnsAsync((Item)null);

			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var actionResult = await controller.CreateItem(saveItemResource);

			// Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task CreateItem_ReturnOkObjectResult_WhenCreateItemIsSuccess()
		{
			//Arrange
			var saveItemResource = new SaveItemResource()
			{
				Title = "Item 1",
                Description = "Description",
				Price = 30.00M,
				Condition = "New",
                Quantity = 1,
				IsAuction = false
			};
            var newItem = _mapper.Map<SaveItemResource, Item>(saveItemResource);

			_mockItemService.Setup(service => service.CreateItem(It.IsAny<Item>()))
				.ReturnsAsync(newItem);

			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var actionResult = await controller.CreateItem(saveItemResource);

			// Assert
			Assert.IsType<OkObjectResult>(actionResult);
		}

		[Fact]
		public async Task UpdateItem_ReturnUnauthorizedResult_WhenUserIsNotTheItemSeller()
		{
			//Arrange
            var itemId = 1;
			var saveItemResource = new SaveItemResource()
			{
				Title = null,
                SellerId = 1
			};
            var item = new Item() { SellerId = 1 };
			_mockItemService.Setup(service => service.GetItemById(itemId))
			.ReturnsAsync(item);
			var controller = new ItemsController(_mockItemService.Object, _mapper);
			var unauthorizedUser = new ClaimsPrincipal(new ClaimsIdentity(
				new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, "2")
				}
				, "TestAuthentication"));
			SetupHttpContextUser(controller, unauthorizedUser);

			// Act
			var actionResult = await controller.UpdateItem(itemId, saveItemResource);

			// Assert
			Assert.IsType<UnauthorizedResult>(actionResult);
		}

		[Fact]
		public async Task UpdateItem_ReturnBadRequestResult_WhenSaveItemResourceIsInvalid()
		{
			//Arrange
            var userId = 1;
            var itemId = 1;
			var saveItemResource = new SaveItemResource()
			{
				Title = null,
            SellerId = userId
			};
         var item = new Item() { SellerId = userId };
			_mockItemService.Setup(service => service.GetItemById(itemId))
			.ReturnsAsync(item);
			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var actionResult = await controller.UpdateItem(itemId, saveItemResource);

			// Assert
			Assert.IsType<BadRequestObjectResult>(actionResult);
		}

		[Fact]
		public async Task UpdateItem_ReturnNotFoundObject_WhenGetItemByIdReturnsNull()
		{
			//Arrange
         var testItemId = 1;
			var itemToBeUpdated = GetTestItems().FirstOrDefault(i => i.Id == testItemId);
			var saveItemResource = new SaveItemResource()
			{
					Title = "Item 1",
					Price = 30.00m,
					Condition = "New",
					IsAuction = false,
               SellerId = 1
			};

			_mockItemService.Setup(service => service.GetItemById(testItemId))
				.ReturnsAsync((Item)null);

			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var result = await controller.UpdateItem(itemToBeUpdated.Id, saveItemResource);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task UpdateItem_ReturnItemResourceInOkObjectResult_WhenUpdateIsSuccess()
		{
			//Arrange
         var itemId = 1;
			var saveItemResource = new SaveItemResource()
			{
				Title = "Updated Item 1",
				Price = 30.00M,
				Condition = "New",
				IsAuction = false,
                Quantity = 1,
                SellerId = 1
			};
			var currentItem = GetTestItems().FirstOrDefault(
				i => i.Id == itemId);
			var expectedItemResource = new ItemResource()
			{
				Id = itemId,
				Title = "Updated Item 1",
				Price = 30.00M,
				Condition = "New",
				IsAuction = false,
				Quantity = 1,
				SellerId = 1,
				ImageUrl = new List<FilePath>()
			};
			var updatedItem = _mapper.Map<ItemResource, Item>(expectedItemResource);

			// subsequent call to GetItemById method to return updatedItem
			bool firstCall = true;
			_mockItemService.Setup(service => service.GetItemById(itemId))
				.ReturnsAsync(() => {
					if (firstCall)
					{
						firstCall = false;
						return currentItem;
					}
					return updatedItem;
				});

			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var result = await controller.UpdateItem(itemId, saveItemResource);
			var objectResult = result as OkObjectResult;
			var value = objectResult.Value as ItemResource;

			// Assert
			Assert.IsType<OkObjectResult>(result);
			Assert.Equal(serializeObject(expectedItemResource), serializeObject(value));
		}

		[Fact]
		public async Task DeleteItem_ReturnUnauthorizedResult_WhenUserIsNotTheItemSeller()
		{
			// Arrange
			var sellerId = 1;
			var item = GetTestItems().FirstOrDefault(i => i.SellerId == sellerId);
			// setup GetItemById to return null
			_mockItemService.Setup(service => service.GetItemById(It.IsAny<int>()))
				.ReturnsAsync(item);
			var controller = new ItemsController(_mockItemService.Object, _mapper);
			var unauthorizedUser = new ClaimsPrincipal(new ClaimsIdentity(
				new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, "2")
				}
				, "TestAuthentication"));
			SetupHttpContextUser(controller, unauthorizedUser);

			// Act
			var actionResult = await controller.DeleteItem(item.Id);

			// Assert
			Assert.IsType<UnauthorizedResult>(actionResult);
		}

		[Fact]
		public async Task DeleteItem_ReturnNotFoundResult_WhenItemIsNotFound()
		{
			// Arrange
			var itemId = 1;
			var item = GetTestItems().FirstOrDefault(i => i.Id == itemId);
			// setup GetItemById to return null
			_mockItemService.Setup(service => service.GetItemById(It.IsAny<int>()))
				.ReturnsAsync((Item)null);
			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var actionResult = await controller.DeleteItem(itemId);

			// Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task DeleteItem_ReturnNoContentResult_WhenDeleteItemIsSuccess()
		{
			// Arrange
			var itemId = 1;
			var item = GetTestItems().FirstOrDefault(i => i.Id == itemId);
			// setup GetItemById to return null
			_mockItemService.Setup(service => service.GetItemById(It.IsAny<int>()))
				.ReturnsAsync(item);
			var controller = new ItemsController(_mockItemService.Object, _mapper);
			SetupHttpContextUser(controller, _user);

			// Act
			var actionResult = await controller.DeleteItem(itemId);

			// Assert
			Assert.IsType<NoContentResult>(actionResult);
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
				Title = "Item 1",
				Price = 30.00M,
				Condition = "New",
				IsAuction = false,
				Quantity = 1,
				SellerId = 1
			});
			itemResources.Add(new ItemResource()
			{
				Id = 2,
				Title = "Item 2",
				Price = 40.00M,
				Condition = "New",
				IsAuction = false,
				Quantity = 1,
				SellerId = 1
			});

			return itemResources;
		}

      private IEnumerable<Item> GetTestItems()
      {
         var items = new List<Item>();
			items.Add(new Item()
			{
				Id = 1,
				Title = "Item 1",
				Price = 30.00M,
				Condition = "New",
				IsAuction = false,
				Quantity = 1,
				SellerId = 1
			});
			items.Add(new Item()
         {
            Id = 2,
				Title = "Item 2",
				Price = 40.00M,
				Condition = "New",
				IsAuction = false,
            Quantity = 1,
				SellerId = 1
         });

         return items;
      }

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T) ((ObjectResult) result.Result).Value;
		}

		private void SetupHttpContextUser(ItemsController controller, ClaimsPrincipal user)
		{
			controller.ControllerContext = new ControllerContext();
			controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
		}
    }
}