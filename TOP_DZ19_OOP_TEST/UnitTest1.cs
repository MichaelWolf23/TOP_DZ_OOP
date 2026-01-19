using Xunit;
using Moq;
using TOP_DZ19_OOP;

namespace TOP_DZ19_OOP_TEST;

public class NotificationServiceTests
{
    [Fact]
    public void NotifyUser_ShouldSend_WhenUserExists()
    {
        // Arrange
        var mockUserRepo = new Mock<IUserRepository>();
        var mockMessageSender = new Mock<IMessageSender>();

        var fakeUser = new User { Id = 1, Name = "Иван", Email = "ivan@test.com" };

        mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))
                    .Returns(fakeUser);

        var service = new NotificationService(mockUserRepo.Object, mockMessageSender.Object);

        // Act
        service.NotifyUser(1, "Ваш заказ готов");

        // Assert
        mockMessageSender.Verify(
            sender => sender.SendMessage("ivan@test.com", "Уважаемый Иван, Ваш заказ готов"),
            Times.Once()
        );
    }

    [Fact]
    public void NotifyUser_ShouldNotSend_WhenUserNotFound()
    {
        // Arrange
        var mockUserRepo = new Mock<IUserRepository>();
        var mockMessageSender = new Mock<IMessageSender>();

        mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))
                    .Returns((User)null);

        var service = new NotificationService(mockUserRepo.Object, mockMessageSender.Object);

        // Act
        service.NotifyUser(999, "Любое сообщение");

        // Assert
        mockMessageSender.Verify(
            sender => sender.SendMessage(It.IsAny<string>(), It.IsAny<string>()),
            Times.Never()
        );
    }
}