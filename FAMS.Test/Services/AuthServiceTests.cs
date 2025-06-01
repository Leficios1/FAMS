using Castle.Core.Configuration;
using FAMS.Api.Services;
using FAMS.Core.Interfaces.Repositories;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.FakeItEasy;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;

namespace FAMS.Test.Services
{
    [TestFixture]   
    // done
    public class AuthServiceTests
    {
        private Mock<IBaseRepository<User>> _mockUserRepo;
        private  Mock<Microsoft.Extensions.Configuration.IConfiguration> _mockConfiguration;
        private AuthService _authService;
        [SetUp] 
        public void SetUp() 
        { 
            _mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _mockUserRepo= new Mock<IBaseRepository<User>>();
            _authService = new AuthService(_mockUserRepo.Object,_mockConfiguration.Object);
        }

        [Test]

        public async Task LoginAccount_ReturnUnauthorized()
        {
            _mockUserRepo.Setup(x => x.Get()).Returns(new List<User>()
            {
                new User { Id = 1,Name="Trinh Huu Tuan", Email="tuanhuu3264@gmail.com", Password="123", PermissionId="SA"}
            }.BuildMock());
            var result = await _authService.LoginAccount(new User() { Email = "tuanhuu3s264@gmail.com", Password = "123" }); 
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
            var obj = (result as UnauthorizedObjectResult).Value as string;

            Assert.IsTrue(obj== "Email/Password is not right.");
        }
        [Test]
        public async Task LoginAccount_ReturnOk()
        {
            _mockUserRepo.Setup(x => x.Get()).Returns(new List<User>()
            {
                new User { Id = 1,Name="Trinh Huu Tuan", Email="tuanhuu3264@gmail.com", Password="123", PermissionId="SA"}
            }.BuildMock());
            var result = await _authService.LoginAccount(new User() { Email = "tuanhuu3264@gmail.com", Password = "123" });
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var obj = (result as OkObjectResult).Value as object;

            Assert.IsNotNull(obj);
        }
        [Test] 
        public async Task GetToken_ReturnHavedValue()
        {
            var user = new User() {
             Name="trinh huu tuan",
             Email="tuanhuu3264@gmail.com",
             Password="123",
             PermissionId="SA"
            };

            var result = _authService.GetToken(user);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JwtSecurityToken>(result);
        }
        [Test]
        public async Task DeCode_ReturnOk()
        {
            var user = new User()
            {
                Name = "trinh huu tuan",
                Email = "tuanhuu3264@gmail.com",
                Password = "123",
                PermissionId = "SA"
            };

            var result = _authService.GetToken(user);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JwtSecurityToken>(result);

            var resultDecode = _authService.DecodeToken(new JwtSecurityTokenHandler().WriteToken(result)); 

            Assert.IsNotNull(resultDecode);
            Assert.IsInstanceOf<ClaimsPrincipal>(resultDecode);
        }
        [Test]
        public async Task GetUser_ReturnOk()
        {
            var user = new User()
            {
                Name = "trinh huu tuan",
                Email = "tuanhuu3264@gmail.com",
                Password = "123",
                PermissionId = "SA"
            };
            _mockUserRepo.Setup(x=>x.Get()).Returns(new List<User>() { user}.BuildMock());  
            

            var result = _authService.GetToken(user);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JwtSecurityToken>(result);

            var resultUser = await _authService.GetUserByToken(new JwtSecurityTokenHandler().WriteToken(result));
            Assert.IsNotNull(resultUser);
            Assert.IsInstanceOf<OkObjectResult>(resultUser);
        }

        [Test]
        public async Task GetUser_ReturnInvalidToken()
        {
            var resultUser = await _authService.GetUserByToken("sadadasdad");
            Assert.IsNotNull(resultUser);
            Assert.IsInstanceOf<BadRequestObjectResult>(resultUser);

            var mess = (resultUser as BadRequestObjectResult).Value as string;

            Assert.IsTrue(mess == "Token is invalid");
        }
        [Test]
        public async Task GetUser_ReturnNotFoundUser()
        {
            var user = new User()
            {
                Name = "trinh huu tuan",
                Email = "tuanhuu3264@gmail.com",
                Password = "123",
                PermissionId = "SA"
            };
            _mockUserRepo.Setup(x => x.Get()).Returns(new List<User>() { }.BuildMock());


            var result = _authService.GetToken(user);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JwtSecurityToken>(result);

            var resultUser = await _authService.GetUserByToken(new JwtSecurityTokenHandler().WriteToken(result));
            Assert.IsNotNull(resultUser);
            Assert.IsInstanceOf<NotFoundObjectResult>(resultUser);

            var mess = (resultUser as NotFoundObjectResult).Value as string;

            Assert.IsTrue(mess == "User not found");
        }
    }
}
