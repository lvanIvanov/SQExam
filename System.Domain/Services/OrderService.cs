using System.Domain.Models;

namespace System.Domain.Services;

public class OrderService
{
    private readonly INotificationService? _notificationService;

    public OrderService(INotificationService? notificationService = null)
    {
        _notificationService = notificationService;
    }

    public void ExecuteOrder(Order order, Trader trader)
    {
        if (order == null || trader == null || order.Stock == null) return;

        decimal totalCost = order.Stock.Price * order.Quantity;

        if (order.Status == OrderStatus.Created && trader.Balance >= totalCost)
        {
            trader.Balance -= totalCost; 
        
            order.Place(); 

            if (order.Status == OrderStatus.Executed)
            {
                _notificationService?.NotifyTrader("Order executed successfully");
            }
        }
        else if (trader.Balance < totalCost)
        {
            order.Reject();
            _notificationService?.NotifyTrader("Insufficient funds");
        }
    }
    
    public static int DetermineOrderRiskLevel(Order order)
    {
        if (order == null) return 99;
        
        return order.Quantity switch
        {
            <= 0 => 99,
            < 10 => 1,
            < 50 => 2,
            < 100 => 3,
            _ => 4
        };
    }
}