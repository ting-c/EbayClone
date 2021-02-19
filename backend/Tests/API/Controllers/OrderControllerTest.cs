using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EbayClone.API.Controllers;
using EbayClone.API.Resources;
using EbayClone.Core.Models;
using EbayClone.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Tests.API.Controllers
{
	public class OrderControllerTests
	{
		private readonly Mock<IItemService> _mockItemService;
		private readonly Mock<IOrderItemService> _mockOrderItemService;
		private readonly Mock<IOrderService> _mockOrderService;
		private readonly ClaimsPrincipal _user;

		public OrderControllerTests()
		{
			this._mockItemService = new Mock<IItemService>();
			this._mockOrderItemService = new Mock<IOrderItemService>();
			this._mockOrderService = new Mock<IOrderService>();
			this._user = new ClaimsPrincipal(new ClaimsIdentity(
				new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, "1")
				}
				, "TestAuthentication"));
		}

		[Fact]
		public async Task GetOrderById_ReturnUnauthorizedResult_WhenUserIsUnauthorized()
		{
			//Arrange
			var userId = 2;
			var order = new Order(userId);

			_mockOrderService.Setup(service => service.GetOrderById(It.IsAny<int>()))
				.ReturnsAsync(order);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.GetOrderById(1);

			//Assert
			Assert.IsType<UnauthorizedResult>(actionResult);
		}

		[Fact]
		public async Task GetOrderById_ReturnOrderInOkObjectResult_WhenSucceeds()
		{
			//Arrange
			var userId = 1;
			var order = new Order(userId);

			_mockOrderService.Setup(service => service.GetOrderById(It.IsAny<int>()))
				.ReturnsAsync(order);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.GetOrderById(1);
			var objectResult = actionResult as OkObjectResult;
			var value = objectResult.Value as Order;

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(order), serializeObject(value));
		}

		[Fact]
		public async Task GetAllByUserId_ReturnNotFoundResult_WhenOrdersIsNull()
		{
			//Arrange
			var orders = new List<Order>();

			_mockOrderService.Setup(service => service.GetAllByUserId(It.IsAny<int>()))
				.ReturnsAsync((List<Order>)null);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.GetAllByUserId(1);

			//Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task GetAllByUserId_ReturnAnEmptyListInOkObjectResult_WhenOrdersIsAnEmptyList()
		{
			//Arrange
			var orders = new List<Order>();

			_mockOrderService.Setup(service => service.GetAllByUserId(It.IsAny<int>()))
				.ReturnsAsync(orders);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.GetAllByUserId(1);
			var objectResult = actionResult as OkObjectResult;
			var value = objectResult.Value as IEnumerable<Order>;

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(orders), serializeObject(value));
		}

		[Fact]
		public async Task GetAllByUserId_ReturnOrdersInOkObjectResult_WhenSucceeds()
		{
			//Arrange
			var userId = 1;
			var orders = new List<Order>()
			{
				new Order(userId),
				new Order(userId)
			};

			_mockOrderService.Setup(service => service.GetAllByUserId(It.IsAny<int>()))
				.ReturnsAsync(orders);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.GetAllByUserId(1);
			var objectResult = actionResult as OkObjectResult;
			var value = objectResult.Value as IEnumerable<Order>;

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
			Assert.Equal(serializeObject(orders), serializeObject(value));
		}

		[Fact]
		public async Task CreateOrder_ReturnNotFoundResult_WhenItemIsNotFound()
		{
			//Arrange
			var basketItems = new List<BasketItem>()
			{
				new BasketItem(10, 1, 1),
				new BasketItem(20, 1, 1)
			};

			var createOrderResource = new CreateOrderResource()
			{
				BasketItems = basketItems
			};
			_mockItemService.Setup(service => service.GetItemById(It.IsAny<int>()))
				.ReturnsAsync((Item)null);
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.CreateOrder(createOrderResource);

			//Assert
			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public async Task CreateOrder_ReturnOkResult_WhenCreateOrderSucceeds()
		{
			//Arrange
			var userId = 1;
			var basketItems = new List<BasketItem>()
			{
				new BasketItem(10, 1, 1),
				new BasketItem(20, 1, 1)
			};

			var createOrderResource = new CreateOrderResource()
			{
				BasketItems = basketItems
			};
			_mockItemService.SetupSequence(service => service.GetItemById(It.IsAny<int>()))
				.ReturnsAsync(new Item())
				.ReturnsAsync(new Item());
			_mockOrderService.Setup(service => service.CreateOrder(It.IsAny<Order>()))
				.ReturnsAsync(new Order(userId));
			var controller = new OrderController(_mockItemService.Object, _mockOrderItemService.Object, _mockOrderService.Object);
			SetupHttpContextUser(controller, _user);

			//Act
			var actionResult = await controller.CreateOrder(createOrderResource);

			//Assert
			Assert.IsType<OkObjectResult>(actionResult);
		}

		private void SetupHttpContextUser(OrderController controller, ClaimsPrincipal user)
		{
			controller.ControllerContext = new ControllerContext();
			controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
		}

		private static T GetObjectResultContent<T>(ActionResult<T> result)
		{
			return (T)((ObjectResult)result.Result).Value;
		}

		private string serializeObject<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

	}
}