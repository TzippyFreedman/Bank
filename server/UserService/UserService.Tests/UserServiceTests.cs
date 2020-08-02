using Moq;
using System;
using System.Threading.Tasks;
using UserService.Contract;
using UserService.Contract.Models;
using UserService.Helpers.Interfaces;
using UserService.Services.Exceptions;
using Xunit;

namespace UserService.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> userRepoMoq = new Mock<IUserRepository>();
        private readonly IAccountRepository accountRepoMoqObject = new Mock<IAccountRepository>().Object;
        private readonly IEmailVerifier emailVerifierMoqObject = new Mock<IEmailVerifier>().Object;
        private readonly IPasswordHasher passwordHasherMoqObject = new Mock<IPasswordHasher>().Object;
        private readonly string password = "123456";

        [Fact]
        public async void AddVerificationAsync_UserAlreadyExists_ThrowsUserWithRequestedEmailAlreadyExistsException()
        {
            //Arrange
            string emailOfExistingUser = "estherivka1@gmail.com";
            userRepoMoq
                    .Setup(repo => repo.IsExistsAsync(emailOfExistingUser))
                    .ReturnsAsync(true);
            Services.UserService userService = new Services.UserService(userRepoMoq.Object, accountRepoMoqObject, emailVerifierMoqObject, passwordHasherMoqObject);

            //Act
            Task act() => userService.AddVerificationAsync(new EmailVerificationModel { Email = emailOfExistingUser });

            //Assert
            await Assert.ThrowsAsync<UserWithRequestedEmailAlreadyExistsException>(act);

        }

        [Fact]
        public async void RegisterAsync_IncorrectVerificationCode_ThrowsIncorrectVerificationCodeException()
        {
            //Arrange
            string emailAddress = "esther@gmail.com";
            string incorrectVerificationCode = "e123";
            userRepoMoq
                    .Setup(repo => repo.IsExistsAsync(emailAddress))
                    .ReturnsAsync(false);
            userRepoMoq
             .Setup(repo => repo.GetVerificationCodeAsync(emailAddress))
             .ReturnsAsync(new EmailVerificationModel { Email = emailAddress, Code = "abcd" });
            Services.UserService userService = new Services.UserService(userRepoMoq.Object, accountRepoMoqObject, emailVerifierMoqObject, passwordHasherMoqObject);

            //Act
            Task act() => userService.RegisterAsync(new UserModel { Email = emailAddress }, password, incorrectVerificationCode);

            //Assert
            await Assert.ThrowsAsync<IncorrectVerificationCodeException>(act);

        }

        [Fact]
        public async void RegisterAsync_VerificationCodeExpired_ThrowsVerificationCodeExpiredException()
        {
            //Arrange
            string emailAddress = "esther@gmail.com";
            string expiredVerificationCode = "e123";
            userRepoMoq
                    .Setup(repo => repo.IsExistsAsync(emailAddress))
                    .ReturnsAsync(false);
            userRepoMoq
                        .Setup(repo => repo.GetVerificationCodeAsync(emailAddress))
                        .ReturnsAsync(new EmailVerificationModel { Email = emailAddress, Code = expiredVerificationCode, ExpirationTime = DateTime.Now });
            Services.UserService userService = new Services.UserService(userRepoMoq.Object, accountRepoMoqObject, emailVerifierMoqObject, passwordHasherMoqObject);

            //Act
            Task act() => userService.RegisterAsync(new UserModel { Email = emailAddress }, password, expiredVerificationCode);

            //Assert
            await Assert.ThrowsAsync<VerificationCodeExpiredException>(act);

        }

        [Fact]
        public async void RegisterAsync_UserAlreadyExists_ThrowsUserWithRequestedEmailAlreadyExistsException()
        {
            //Arrange
            string emailOfExistingUser = "estherivka1@gmail.com";
            string verificationCode = "a345";
            userRepoMoq
                    .Setup(repo => repo.IsExistsAsync(emailOfExistingUser))
                    .ReturnsAsync(true);
            Services.UserService userService = new Services.UserService(userRepoMoq.Object, accountRepoMoqObject, emailVerifierMoqObject, passwordHasherMoqObject);

            //Act
            Task act() => userService.RegisterAsync(new UserModel { Email = emailOfExistingUser }, password, verificationCode);

            //Assert
            await Assert.ThrowsAsync<UserWithRequestedEmailAlreadyExistsException>(act);

        }

        [Fact]
        public async void GetByIdAsync_UserExists_ReturnsUserModel()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            userRepoMoq
                    .Setup(repo => repo.GetByIdAsync(userId))
                    .ReturnsAsync(new UserModel { Id = userId });
            Services.UserService userService = new Services.UserService(userRepoMoq.Object, accountRepoMoqObject, emailVerifierMoqObject, passwordHasherMoqObject);

            //Act
            var user = await userService.GetByIdAsync(userId);

            //Assert
            Assert.IsType<UserModel>(user);

        }
    }
}
