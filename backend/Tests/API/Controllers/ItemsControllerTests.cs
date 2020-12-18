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
        private readonly IMapper _mapper;
        public ItemsControllerTests()
        {
            // create mapper using MappingProfile in API
			var mappingProfile = new MappingProfile();
			var config = new MapperConfiguration(config => 
                config.AddProfile(mappingProfile));
            this._mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllItems_ReturnWithAListOfItemResources()
        {
            var expectedObject = GetTestItemResources();

            //Arrange
            // mock IItemService and its GetAllWithUser method
            var mockService = new Mock<IItemService>();
            mockService.Setup(service => service.GetAllWithUser())
                .ReturnsAsync(GetTestItems());
            
            // inject mocked IItemService and _mapper in controller
            var controller = new ItemsController(mockService.Object, _mapper);
            
            //Act
            var actionResult = await controller.GetAllItems();
            var resultObject = GetObjectResultContent<IEnumerable<ItemResource>>(actionResult);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<ItemResource>>>(actionResult);
            Assert.Equal(serializeObject(expectedObject), serializeObject(resultObject));
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