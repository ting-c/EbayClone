using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EbayClone.API.Controllers;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Tests.API.Controllers
{
    public class BasketItemControllerTests
    {
		private readonly Mock<IBasketItemService> _mockService;
		private readonly ClaimsPrincipal _user;

			public BasketItemControllerTests()
			{
					this._mockService = new Mock<IBasketItemService>();
					this._user = new ClaimsPrincipal(new ClaimsIdentity(
						new Claim[]
						{
							new Claim(ClaimTypes.NameIdentifier, "1")
						}
						, "TestAuthentication"));
			}

			[Fact]
			public async Task GetAllBasketItems_ReturnNotFoundResult_whenBasketItemsIsNull()
			{
					//Arrange
					var basketItems = new List<BasketItem>()
						{
							new BasketItem(10, 1, 1),
							new BasketItem(20, 1, 1)
						};
					
					_mockService.Setup(service => service.GetAllBasketItems(It.IsAny<int>()))
						.ReturnsAsync((IEnumerable<BasketItem>)null);
					// inject mocked IItemService and _mapper in controller
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.GetAllBasketItems();

					//Assert
					Assert.IsType<NotFoundResult>(actionResult);
			}

			[Fact]
			public async Task GetAllBasketItems_ReturnBasketItemsInOkObjectResult_WhenGetBasketItemsIsSuccess()
			{
					//Arrange
					var basketItems = new List<BasketItem>()
						{
							new BasketItem(10, 1, 1),
							new BasketItem(20, 1, 1)
						};
					
					_mockService.Setup(service => service.GetAllBasketItems(It.IsAny<int>()))
						.ReturnsAsync(basketItems);
					// inject mocked IItemService and _mapper in controller
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.GetAllBasketItems() as OkObjectResult;
					var resultObject = GetObjectResultContent<IEnumerable<BasketItem>>(actionResult);

					//Assert
					Assert.IsType<OkObjectResult>(actionResult);
					Assert.Equal(serializeObject(basketItems), serializeObject(resultObject));
			}

			[Fact]
			public async Task AddBasketItem_ReturnBadRequestResult_WhenQuantityIsInvalid()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.AddBasketItem(It.IsAny<BasketItem>()))
						.ReturnsAsync(newBasketItem);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.AddBasketItem(10, 51);

					//Assert
					Assert.IsType<BadRequestObjectResult>(actionResult);
			}

			[Fact]
			public async Task AddBasketItem_ReturnNotFoundResult_WhenBasketItemIsNull()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.AddBasketItem(It.IsAny<BasketItem>()))
						.ReturnsAsync((BasketItem) null);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.AddBasketItem(10, 1);

					//Assert
					Assert.IsType<NotFoundResult>(actionResult);
			}

			[Fact]
			public async Task AddBasketItem_ReturnBasketItemInOkObjectResult_WhenAddBasketItemIsSuccess()
			{
				//Arrange
				var newBasketItem = new BasketItem(10, 1, 1);
				
				_mockService.Setup(service => service.AddBasketItem(It.IsAny<BasketItem>()))
					.ReturnsAsync(newBasketItem);
				var controller = new BasketItemController(_mockService.Object);
					SetupHttpContextUser(controller, _user);

				//Act
				var actionResult = await controller.AddBasketItem(10, 1) as OkObjectResult;
				var resultObject = GetObjectResultContent<BasketItem>(actionResult);

				//Assert
				Assert.IsType<OkObjectResult>(actionResult);
				Assert.Equal(serializeObject(newBasketItem), serializeObject(resultObject));
			}

			[Fact]
			public async Task RemoveBasketItem_ReturnNotFoundResult_WhenBasketItemIsNull()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync((BasketItem)null);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.RemoveBasketItem(10);

					//Assert
					Assert.IsType<NotFoundResult>(actionResult);
			}

			[Fact]
			public async Task RemoveBasketItem_ReturnUnauthorizedResult_WhenUserIsNotBasketItemOwner()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 2, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync(newBasketItem);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.RemoveBasketItem(10);

					//Assert
					Assert.IsType<UnauthorizedResult>(actionResult);
			}

			[Fact]
			public async Task RemoveBasketItem_ReturnOkResult_WhenRemoveBasketItemIsSuccess()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync(newBasketItem);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.RemoveBasketItem(10);

					//Assert
					Assert.IsType<OkResult>(actionResult);
			}

			[Fact]
			public async Task UpdateQuantity_ReturnNotFoundResult_WhenBasketItemIsNull()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync((BasketItem)null);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.UpdateQuantity(10, 2);

					//Assert
					Assert.IsType<NotFoundResult>(actionResult);
			}

			[Fact]
			public async Task UpdateQuantity_ReturnUnauthorizedResult_WhenUserIsNotBasketItemOwner()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 2, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync(newBasketItem);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.UpdateQuantity(10, 2);

					//Assert
					Assert.IsType<UnauthorizedResult>(actionResult);
			}

			[Fact]
			public async Task UpdateQuantity_ReturnOkResult_WhenUpdateQuantityIsSuccess()
			{
					//Arrange
					var newBasketItem = new BasketItem(10, 1, 1);
					
					_mockService.Setup(service => service.GetBasketItemById(It.IsAny<int>()))
						.ReturnsAsync(newBasketItem);
					var controller = new BasketItemController(_mockService.Object);
						SetupHttpContextUser(controller, _user);

					//Act
					var actionResult = await controller.UpdateQuantity(10, 2);

					//Assert
					Assert.IsType<OkResult>(actionResult);
			}

			private static T GetObjectResultContent<T>(ActionResult<T> result)
			{
				return (T)((ObjectResult)result.Result).Value;
			}

			private void SetupHttpContextUser(BasketItemController controller, ClaimsPrincipal user)
			{
				controller.ControllerContext = new ControllerContext();
				controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
			}
			private string serializeObject<T>(T obj)
			{
				return JsonConvert.SerializeObject(obj);
			}
    }
}