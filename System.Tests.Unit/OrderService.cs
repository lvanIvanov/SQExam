using System.Domain.Services;
using System.Domain.Models;
using Moq;

namespace System.Tests.Unit;

[TestFixture]
public class OrderServiceTests
{
    [Test]
    public void ExecuteOrder_ExecutedOrder_SendsNotification()
    {
        var mockNotification = new Mock<INotificationService>();
        var service = new OrderService(mockNotification.Object);

        var trader = new Trader("John", 10000);
        var stock = new Stock("AAPL", 100);
        var order = new Order(stock, 10, OrderType.Buy);

        service.ExecuteOrder(order, trader);

        mockNotification.Verify(
            n => n.NotifyTrader(It.IsAny<string>()),
            Times.Once);
    }
}
