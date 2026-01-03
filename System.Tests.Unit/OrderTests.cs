using NUnit.Framework;
using System.Domain.Models;

namespace System.Tests.Unit
{
    [TestFixture]
    public class OrderTests
    {
        private Stock _stock;

        [SetUp]
        public void Setup()
        {
            _stock = new Stock("AAPL", 100.00m);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Place_InvalidQuantity_OrderRejected(int quantity)
        {
            var order = new Order(_stock, quantity, OrderType.Buy);

            order.Place();

            // Assert
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Rejected));
        }

        [Test]
        public void Place_ValidQuantity_OrderExecuted()
        {
            var order = new Order(_stock, 10, OrderType.Buy);

            order.Place();
            
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Executed));
        }
    }
}