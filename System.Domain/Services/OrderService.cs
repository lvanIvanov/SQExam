using System.Domain.Models;

namespace System.Domain.Services;


public class OrderService
{
    private readonly INotificationService _notificationService;
    private const string ADMIN_ROLE = "ADMIN";

    public OrderService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void ExecuteOrder(Order order, Trader trader)
    {
        try
        {
            order.Place();

            if (order.Status == OrderStatus.Executed)
            {
                _notificationService.NotifyTrader("Order executed");
            }
        }
        catch
        {
            // empty catch block (intentional)
        }
    }

    public string GetOrdersBySymbol(string symbol)
    {
        // SQL injection vulnerability (intentional)
        return "SELECT * FROM Orders WHERE Symbol = '" + symbol + "'";
    }

    public int DetermineOrderRiskLevel(Order order)
    {
        if (order.Quantity < 10)
            return 1;
        else if (order.Quantity < 50)
            return 2;
        else if (order.Quantity < 100)
            return 3;
        else if (order.Quantity < 500)
            return 4;
        else
            return 5;
    }
}
