using System.Domain.Services;
using System.Domain.Models;
using Moq;

namespace System.Tests.Unit;

[TestFixture]
public class OrderServiceTests
{
    [Test]
    public void ExecuteOrder_CallsNotificationService_OnSuccess()
    {
        // Arrange
        var mockNotify = new Mock<INotificationService>();
        var service = new OrderService(mockNotify.Object);
        var trader = new Trader("John", 1000m);
        var order = new Order(new Stock("TSLA", 100m), 1, OrderType.Buy);

        // Act
        service.ExecuteOrder(order, trader);

        // Assert
        mockNotify.Verify(n => n.NotifyTrader(It.IsAny<string>()), Times.Once);
    }
    
    [Test]
    [TestCase(-1, 99)]
    [TestCase(5, 1)]
    [TestCase(20, 2)]
    [TestCase(75, 3)]
    [TestCase(150, 4)]
    public void DetermineOrderRiskLevel_ReturnsCorrectLevel(int quantity, int expectedLevel)
    {
        // Arrange
        var stock = new Stock("AAPL", 150m);
        var order = new Order(stock, quantity, OrderType.Buy);

        // Act
        var result = OrderService.DetermineOrderRiskLevel(order);

        // Assert
        Assert.That(result, Is.EqualTo(expectedLevel));
    }
}
