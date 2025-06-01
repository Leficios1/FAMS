using AutoMapper;
using FAMS.Api.Controllers;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Request;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;

namespace FAMS.Test.Controllers
{
    public class UsersControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<IMapper> _mockMapper;
        private UsersController _usersController;
        private Mock<IPasswordGenerator> _mockPasswordGenerator;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _mockPasswordGenerator = new Mock<IPasswordGenerator>();
            _usersController = new UsersController(_mockUserService.Object, _mockMapper.Object,
                _mockPasswordGenerator.Object);
        }

        [Test]
        public async Task UsersController_GetUser_ReturnsOk()
        {
            // Arrange
            _mockUserService.Setup(x => x.GetUser(1)).ReturnsAsync(new UserResponseDto());
            // Act
            var result = await _usersController.GetUser(1);
            // Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkResult");
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode, "Status code should be 200");
        }
        [Test]
        public async Task UsersController_CreateUser_ReturnsNotFound()
        {
            //Arrange
            var userReq = new CreateUserDTO();
            _mockMapper.Setup(x => x.Map<User>(userReq)).Returns(new User());
            //Act
            var result = await _usersController.CreateUser(userReq);
            //Assert
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<NotFoundObjectResult>(result, "Result should be an instance of NotFoundObjectResult");
            Assert.AreEqual(404, ((NotFoundObjectResult)result).StatusCode, "Status code should be 404");
        }
        [Test]
        public async Task UserController_CreateUser_ReturnOk()
        {
            var userReq = new CreateUserDTO()
            {
                Email = "VDkhoa@gmail.com",
                Name = "VDkhoa",
                Phone = "0987123456",
                DateOfBirth = "23/06/2003",
                Gender = "male",
                Rolename = "Admin",
                Status = true,
                CreatedBy = "KhoaVD",
            };
            _mockUserService.Setup(x => x.CreateUser(userReq)).ReturnsAsync(new UserResponseDto());
            _mockMapper.Setup(x=>x.Map<User>(userReq)).Returns(new User());
            var result= await _usersController.CreateUser(userReq);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(200, ((OkObjectResult)result).StatusCode, "Status code should be 404");
        }
        [Test]
        public async Task UserController_SearchUser_ReturnNotFound()
        {
            string nonExistentString = "odgfn";
            _mockUserService.Setup(x => x.SearchUsers(1, 1, nonExistentString, null, null, null, null, null, null)).ReturnsAsync(new NotFoundResult());
            var result = await _usersController.SearchUsers(1, 1, nonExistentString, null, null);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<NotFoundResult>(result, "Result should be an instance of NotFoundResult");

            var notFoundResult = (NotFoundResult)result;
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code should be 404");
        }
        [Test]
        public async Task UserController_SearchUser_ReturnOk()
        {
            string ExistentString = "Super Admin";
            var data = new UserResponseDto
            {
                PermissionId = "SA",
                Phone = null,
                DateOfBirth = "01/01/2001",
                Gender = "Male",
                Id = 1,
                Name = "Super Admin",
                Email = "superadmin@example.com",
                RoleName = "Super Admin"
            };
            _mockUserService.Setup(x => x.SearchUsers(1, 1, ExistentString, null, null, null, null, null, null)).ReturnsAsync(new OkObjectResult(data));
            var result = await _usersController.SearchUsers(1, 1, ExistentString, null, null);
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsInstanceOf<OkObjectResult>(result, "Result should be an instance of OkResult");
            var OkResult = (OkObjectResult)result;
            Assert.AreEqual(200, OkResult.StatusCode, "Status code should be 200");
        }
    }
}
