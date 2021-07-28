using System;
using Xunit;
using FakeItEasy;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Application.Entities.Authentication.Command.SignUpIndividual;
using System.Threading.Tasks;
using Application.Interfaces.IServices;

namespace Application.Tests.DefaultDataAccessProviderTests
{
    /// <summary>
    /// Test authentication repository
    /// </summary>
    public class DefaultDataAccessAuthRepostoryTest
    {
        /// <summary>
        /// Sign up a user
        /// </summary>
        [Fact]
        public async Task Sign_Up_A_User()
        {
            // Arrange
            var authrepo = A.Fake<IDefaultDataAccessAuthRepository>();

            var command = new SignUpIndividualCommand {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "koko@gmail.com",
                Username = "johnDoe",
                DateOfBirth = DateTime.Now.AddYears(-15),
                Password = "String"
            };

            // Act
            // var result = await authrepo.SignUpIndividual(command);

            // // Assert
            // Assert.NotNull(result);
        }
    }
}