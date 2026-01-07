using System.Domain.Models;
using System.Domain.Services;

namespace System.Tests.Unit;

[TestFixture]
public class OrderBoundaryTests
{
    [Test]
    public void ExecuteOrder_ExactBalance_SuccessfullyCompletes()
    {
        // Arrange: Trader has exactly enough for 10 shares at $10
        var trader = new Trader("John", 100m); 
        var stock = new Stock("AAPL", 10m);
        var order = new Order(stock, 10, OrderType.Buy);
        var service = new OrderService();

        // Act
        service.ExecuteOrder(order, trader);

        // Assert
        Assert.That(trader.Balance, Is.EqualTo(0m));
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Executed));
    }

    [Test]
    public void ExecuteOrder_OneCentShort_RejectsOrder()
    {
        // Arrange: Trader is $0.01 short
        var trader = new Trader("John", 99.99m);
        var stock = new Stock("AAPL", 10m);
        var order = new Order(stock, 10, OrderType.Buy);
        var service = new OrderService();

        // Act
        service.ExecuteOrder(order, trader);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Rejected));
            Assert.That(trader.Balance, Is.EqualTo(99.99m));
        });
    }
}