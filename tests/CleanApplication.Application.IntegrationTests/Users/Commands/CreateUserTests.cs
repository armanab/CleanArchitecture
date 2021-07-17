using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Users.Commands.Create;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using static CleanApplication.Application.IntegrationTests.Testing;

namespace CleanApplication.Application.IntegrationTests.Users.Commands
{
    public class CreateUserTests : TestBase
    {
        private readonly Mock<ICacheService> _cacheService;
        public CreateUserTests()
        {
            _cacheService = new Mock<ICacheService>();
        }

        [Test]
        public async Task ShouldCreateUser()
        {
            var _smscode = "2998";

            var command = new CreateUserCommand
            {
                DateOfBirth = new DateTime(1985, 12, 2),
                Email = "r.man.abi@gmail.com",
                FirstName = "arman",
                Gender = 1,
                LastName = "abi",
                Password = "ArmanAB1!",
                PhoneNumber = "09121060002",
                SmsCode = _smscode,
                UserName = "r.man.abi@gmail.com"
            };

            var cacheKey = string.Format(Constants.RegisterCode_CreateUser, command.PhoneNumber);
            var userId = await RunAsDefaultUserAsync();

            _cacheService.Object.Set(cacheKey, _smscode, TimeSpan.FromMinutes(2));
                
            var result = await SendAsync(command);

            //var list = await FindAsync<ApplicationUser>();

            result.Should().NotBeNull();
            result.Succeeded.Should().Be(true);
            result.Data.PhoneNumberConfirmed.Should().Be(true);
            //list.CreateDate.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
