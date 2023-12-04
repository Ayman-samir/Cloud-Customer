using CloudCustomers.API.Config;
using CloudCustomers.API.Model;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    [TestFixture]
    public class TestUserService
    {
        //[Fact]
        [Test]
        public async Task GetAllusers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut=new UserService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock.
                Protected().
                Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        //[Fact]
        [Test]
        public async Task GetAllusers_WhenCalled_ReturnListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
           var result= await sut.GetAllUsers();

            //Assert
            result.Should().BeOfType<List<User>>();
        }

        //[Fact]
        [Test]
        public async Task GetAllusers_WhenHits404_ReturnEmptyListOfUsers()
        {
            //Arrange
            
            var handlerMock = MockHttpMessageHandler<User>.SetUpReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
        }

        //[Fact]
        [Test]

        public async Task GetAllusers_WhenCalled_ReturnListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers(); 
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        // [Fact]
        [Test]
        public async Task GetAllusers_WhenCalled_InvokedConfigureedExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://jsonplaceholder.typicode.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetUpBasicGetResourceList(expectedResponse,endpoint);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            }) ;
            var sut = new UserService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);

            //Assert
            handlerMock.
                Protected().
                Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri==uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
