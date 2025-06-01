using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using FAMS.Api.Controllers;
using FAMS.Api.Dtos;
using FAMS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Test.Controllers
{
    [TestFixture]

    public class UserPermissionControllerTests
    {
        private Mock<IUserPermissionService> _mockUserPermissionService;
        private Mock<IMapper> _mockMapper;
        private UserPermissionsController _usersController;


        [SetUp]
        public void Setup()
        {
            _mockUserPermissionService = new Mock<IUserPermissionService>();
            _mockMapper = new Mock<IMapper>();

            _usersController = new UserPermissionsController(_mockUserPermissionService.Object, _mockMapper.Object);
        }
        [Test]
        public async Task UserPermissionController_GetAll()
        {
            var result = await _mockUserPermissionService.Object.GetUserPermissionsAsync();

            Assert.IsNotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var data = okResult.Value as dynamic;
            Assert.IsNotNull(data);

            var message = data.Message as string;
            Assert.IsNotNull(data);

            Assert.IsTrue(message == "Get user permissions successfully" || message == "Not found any user permissions",
                          $"Unexpected message: {message}");

            if (message.Equals("Not found any user permissions")) return;

            var objects = data.Objects as List<UserPermissionResponseDto>;
            Assert.IsNotNull(objects);
        }
        [Test]

        public async Task UserPermissionController_UpodatePermissions_WhenEmptyList()
        {
            var permissions = new UpdatePermissionDto[] { };

            var result = await _mockUserPermissionService.Object.UpdatePermission(permissions);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;

            var data = badRequestResult as dynamic;
            Assert.IsNotNull(data);

            var message = data.Message as string;
            Assert.IsTrue(message == "There is no permission in the request.", $"Unexpected message: {message}");
        }
        [Test]

        public async Task UserPermissionController_UpdatePermissions_WhenNotFound()
        {
            var permissions = new UpdatePermissionDto[] { new UpdatePermissionDto { PermissionId = "@@@" } };

            var result = await _mockUserPermissionService.Object.UpdatePermission(permissions);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);

            var badRequestResult = result as BadRequestObjectResult;

            var data = badRequestResult as dynamic;
            Assert.IsNotNull(data);

            var message = data.Message as string;
            Assert.IsTrue(message == $"There has no permission that has id: '{permissions[0].PermissionId} .'", $"Unexpected message: {message}");
        }
    }
}
