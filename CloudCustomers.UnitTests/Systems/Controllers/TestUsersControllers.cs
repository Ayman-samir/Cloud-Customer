using CloudCustomers.API.Controllers;
using CloudCustomers.API.Model;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Xunit;
namespace CloudCustomers.UnitTests.Systems.Controllers;

 [TestFixture]
public class TestUsersControllers
{
    //[Fact]
    [Test]
    public async Task Get_OSuccess_ReturnsStatusCode200()
    {
        //Arrnge
        var mockUsersSerivce = new Mock<IUserService>();
        mockUsersSerivce
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers);
        var sut = new UsersController(mockUsersSerivce.Object);

        //act
        var result = (OkObjectResult)await sut.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    //[Fact]
    [Test]
    public async Task Get_OnSuccess_InvokesUserSerivce()
    {
        //Arrnge
        var mockUsersSerivce = new Mock<IUserService>();
        mockUsersSerivce
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersSerivce.Object);
        //act
        var result = await sut.Get();

        //Assert
        mockUsersSerivce.Verify(service=>service.GetAllUsers(),Times.Once);
    }


    /// <summary>
    /// [Fact]
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        //Arrnge
        var mockUsersSerivce = new Mock<IUserService>();
        mockUsersSerivce
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers);
        var sut = new UsersController(mockUsersSerivce.Object);

        //act
        var result = await sut.Get();


        //Assert
        result.Should().BeOfType<OkObjectResult>();//make sure the response is ok 
        var objectResult=(OkObjectResult)result; //
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    //[Fact]
    [Test]
    public async Task Get_NoUsers_ReturnsStatusCode404()
    {
        //Arrnge
        var mockUsersSerivce = new Mock<IUserService>();
        mockUsersSerivce
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersSerivce.Object);

        //act
        var result = await sut.Get();

        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objResult = (NotFoundResult) result;
        objResult.StatusCode.Should().Be(404);
        
    }



}