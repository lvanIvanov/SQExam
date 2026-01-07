using System.Domain.Models;
using System.Domain.Services;
using NUnit.Framework;
using Reqnroll;
using Moq;

namespace System.Tests.BDD.StepDefinitions;

[Binding]
public class SystemStepDefinitions
{
    private Trader? _trader;
    private Stock? _stock;
    private Order? _order;
    private OrderService? _service;
    private Mock<INotificationService>? _mockNotification;
    private Exception? _lastException;

    [Given("a trader with sufficient balance")]
    public void GivenATraderWithSufficientBalance()
    {
        _trader = new Trader("John", 10000m);
        _mockNotification = new Mock<INotificationService>();
        _service = new OrderService(_mockNotification.Object);
    }

    [Given("a valid stock")]
    public void GivenAValidStock()
    {
        _stock = new Stock("AAPL", 150m);
    }

    [When("the trader places a buy order")]
    public void WhenTheTraderPlacesABuyOrder()
    {
        if (_service == null || _trader == null || _stock == null)
            throw new InvalidOperationException("Test state not initialized properly.");

        _order = new Order(_stock, 10, OrderType.Buy);
        _service.ExecuteOrder(_trader, _order);
    }
    
    [When(@"I attempt to place a Buy order for (.*) shares of (.*)")]
    public void WhenIAttemptToPlaceAnOrder(int quantity, string ticker)
    {
        if (_service == null) 
            _service = new OrderService(new Mock<INotificationService>().Object);
        
        if (_trader == null) 
            _trader = new Trader("Default", 1000m);

        try 
        {
            _stock = new Stock(ticker, 100m);
            _order = new Order(_stock, quantity, OrderType.Buy);
            _service.ExecuteOrder(_trader, _order);
        }
        catch (Exception ex)
        {
            _lastException = ex; 
        }
    }
    
    [When("the order is executed")]
    public void WhenTheOrderIsExecuted()
    {
        if (_order == null || _service == null || _trader == null) 
            throw new InvalidOperationException("Context not initialized: Ensure Given steps ran correctly.");

        _service.ExecuteOrder(_trader, _order);
    }

    [Then("the order should be executed")]
    public void ThenTheOrderShouldBeExecuted()
    {
        if (_order == null) throw new InvalidOperationException("Order was never created.");
        
        Assert.That(_order.Status, Is.EqualTo(OrderStatus.Executed));
    }

    [Then(@"the system should flag an error")]
    public void ThenTheSystemShouldFlagAnError()
    {
        Assert.That(_lastException, Is.Not.Null, "Expected an exception but none was thrown.");
    }
    
    [Given("a trader")]
    public void GivenATrader()
    {
        _trader = new Trader("Guest", 500m);
        _stock = new Stock("MSFT", 200m);
        _service = new OrderService(new Mock<INotificationService>().Object);
    }

    [When("the trader places an order with quantity {int}")]
    public void WhenTheTraderPlacesAnOrderWithQuantity(int quantity)
    {
        if (_service == null || _trader == null || _stock == null)
            throw new InvalidOperationException("Test context missing.");

        _order = new Order(_stock, quantity, OrderType.Buy);
        try {
            _service.ExecuteOrder(_trader, _order);
        } catch (Exception ex) {
            _lastException = ex;
        }
    }

    [Then("the order should be rejected")]
    public void ThenTheOrderShouldBeRejected()
    {
        if (_order == null) throw new InvalidOperationException("No order to verify.");
        Assert.That(_order.Status, Is.EqualTo(OrderStatus.Rejected));
    }
    
    [Given("a trader and notification service")]
    public void GivenATraderAndNotificationService()
    {
        _mockNotification = new Mock<INotificationService>();
        _service = new OrderService(_mockNotification.Object);
        _trader = new Trader("John", 10000m);
        _stock = new Stock("AAPL", 100m);
        _order = new Order(_stock, 1, OrderType.Buy);
    }

    [Then("the trader should receive a notification")]
    public void ThenTheTraderShouldReceiveANotification()
    {
        if (_mockNotification == null) throw new InvalidOperationException("Mock not setup.");
        
        _mockNotification.Verify(n => n.NotifyTrader(It.IsAny<string>()), Times.Once);
    }
}