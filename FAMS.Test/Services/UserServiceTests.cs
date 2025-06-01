using AutoMapper;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Models.Dtos.Response;
using FAMS.Domain.Models.Entities;
using Moq;

using Microsoft.EntityFrameworkCore.Query.Internal;
using MockQueryable.Moq;
using FAMS.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[TestFixture]
public class UserServiceTests
{
    private Mock<IBaseRepository<User>> mockBaseRepository;
    private Mock<IUserRepo> mockUserRepo;
    private Mock<IPasswordGenerator> mockPasswordGenerator;
    private Mock<IMapper> mockMapper;
    private Mock<IBaseRepository<ClassUser>> mockClassUserRepo;
    private Mock<IBaseRepository<UserPermission>> mockUserPermission;
    UserService userService;

    [SetUp]
    public void SetUp()
    {
        mockBaseRepository = new Mock<IBaseRepository<User>>();
        mockUserRepo = new Mock<IUserRepo>();
        mockPasswordGenerator = new Mock<IPasswordGenerator>();
        mockMapper = new Mock<IMapper>();
        mockClassUserRepo = new Mock<IBaseRepository<ClassUser>>();
        mockUserPermission = new Mock<IBaseRepository<UserPermission>>();
       userService = new UserService(mockBaseRepository.Object,mockUserRepo.Object,mockPasswordGenerator.Object,mockMapper.Object,mockClassUserRepo.Object,mockUserPermission.Object);
    }
    [Test]
    public async Task UserService_GetById_ReturnNotNull()
    {
        List<User> users = new List<User>() 
        { 
           new User(){Id=1,Email="tuanhuu3264@gmail.com"},
           new User(){Id=2,Email="tusanhuu3264@gmail.com"},
        };
        var mock = users.BuildMock();

        mockMapper.Setup(x => x.Map<UserResponseDto>(It.IsAny<User>())).Returns((User user )=>new UserResponseDto()
        {
            Id=user.Id,
            Email=user.Email
        });

        mockBaseRepository.Setup(x => x.Get()).Returns(mock);
        
        var result = await userService.GetUser(1); 

        Assert.NotNull(result);
    }
    [Test]
    public async Task SearchUsers_ReturnAllList()
    {
        // Arrange
        var users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), UserPermission = new UserPermission { RoleName = "Admin" } },
        new User { Id = 2, Name = "Jane Smith", Email = "janesmith@example.com", Gender = "Female", DateOfBirth = new DateTime(1995, 5, 5), UserPermission = new UserPermission { RoleName = "User" } },
        new User { Id = 3, Name = "Bob Johnson", Email = "bobjohnson@example.com", Gender = "Male", DateOfBirth = new DateTime(1985, 10, 10), UserPermission = new UserPermission { RoleName = "User" } }
    };
        var mock = users.BuildMock();
        mockBaseRepository.Setup(x => x.Get()).Returns(mock);
        var result = await userService.SearchUsers(1, 10);
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var viewListResponse = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, viewListResponse.PageNumber);
        Assert.AreEqual(10, viewListResponse.PageSize);
        Assert.AreEqual(1, viewListResponse.TotalPage);
        var userList = viewListResponse.List;
        Assert.AreEqual(3, userList.Length);
    }
    [Test]
    public async Task SearchUsers_ReturnSearchResults()
    {
        // Arrange
        var users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), UserPermission = new UserPermission { RoleName = "Admin" } },
        new User { Id = 2, Name = "Jane Smith", Email = "janesmith@example.com", Gender = "Female", DateOfBirth = new DateTime(1995, 5, 5), UserPermission = new UserPermission { RoleName = "User" } },
        new User { Id = 3, Name = "Bob Johnson", Email = "bobjohnson@example.com", Gender = "Male", DateOfBirth = new DateTime(1985, 10, 10), UserPermission = new UserPermission { RoleName = "User" } }
    };
        var mock = users.BuildMock();
        mockBaseRepository.Setup(x => x.Get()).Returns(mock);
        var result = await userService.SearchUsers(1, 10, "John", "Male", "Admin", "1980-01-01", "2000-12-31", "Name", "ASC");
        Assert.IsInstanceOf<OkObjectResult>(result); 
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var viewListResponse = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, viewListResponse.PageNumber);
        Assert.AreEqual(10, viewListResponse.PageSize);
        Assert.AreEqual(1, viewListResponse.TotalPage); 
        var userList = viewListResponse.List;
        Assert.AreEqual(1, userList.Length);
    }
    [Test]
    public async Task SearchUsers_SearchBySearchInput_ReturnOk()
    {
        // Arrange
        var searchInput = "John";
        var users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Gender = "Male", DateOfBirth = new DateTime(1990, 1, 1), UserPermission = new UserPermission { RoleName = "Admin" } },
        new User { Id = 2, Name = "Jane Smith", Email = "janesmith@example.com", Gender = "Female", DateOfBirth = new DateTime(1995, 5, 5), UserPermission = new UserPermission { RoleName = "User" } },
        new User { Id = 3, Name = "Bob Johnson", Email = "bobjohnson@example.com", Gender = "Male", DateOfBirth = new DateTime(1985, 10, 10), UserPermission = new UserPermission { RoleName = "User" } }
    };
        var mock = users.BuildMock();
        mockBaseRepository.Setup(x => x.Get()).Returns(mock);
        var result = await userService.SearchUsers(1, 10, searchInput);
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        Assert.IsInstanceOf<ViewListResponse>(okResult.Value);
        var viewListResponse = (ViewListResponse)okResult.Value;
        Assert.AreEqual(1, viewListResponse.PageNumber);
        Assert.AreEqual(10, viewListResponse.PageSize);
        Assert.AreEqual(1, viewListResponse.TotalPage);
        var userList = viewListResponse.List;
        Assert.AreEqual(2, userList.Length);
    }
}