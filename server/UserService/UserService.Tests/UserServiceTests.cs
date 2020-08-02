using System;
using Xunit;

namespace UserService.Tests
{
    public class UserServiceTests
    {

        IUserRepository userRepository;
        Guid userFileId;

        private void SetupMocks()
        {
            // 1. Create moq object
            var userRepoMoq = new Mock<IUserRepository>();
            userFileId = Guid.NewGuid();
            // 2. Setup the returnables
            userRepoMoq
            .Setup(o => o.GetUserFileById(It.IsAny<Guid>()))
            .ReturnsAsync(new UserFileModel { Id = userFileId });

            // 3. Assign to Object when needed
            // userRepository = userRepository.Object;
            userRepository = userRepoMoq.Object;
        }


        [Fact]
        public async void GetUserFileById_UserFileExists_ReturnsUserFile()
        {
            SetupMocks();
            UserService userService = new UserService(userRepository);

            var userFile = await userService.GetUserFileById(userFileId);

            //Assert.IsType(typeof(UserFileModel), userFile);
            Assert.True(userFile.Id == userFileId);

        }
    }
}
