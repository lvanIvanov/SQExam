using System.Domain.Models;

namespace System.Domain.Services;

public class OrderService
{
    private readonly INotificationService? _notificationService;

    public OrderService(INotificationService? notificationService = null)
    {
        _notificationService = notificationService;
    }

    public void ExecuteOrder(Trader trader, Order order)
    {
        if (order.Status != OrderStatus.Created) return;
    
        decimal totalCost = order.Quantity * order.Stock.Price;
        if (trader.Balance < totalCost) 
        {
            order.Reject();
            _notificationService?.NotifyTrader("Insufficient funds");
            return;
        }
        
        trader.DeductBalance(totalCost);
        order.Place();
        _notificationService?.NotifyTrader("Order Executed Successfully");
    }
    
    public static string DetermineOrderRiskLevel(Order order) =>
        order.Quantity switch
        {
            <= 0  => "Invalid",   // Guard case
            < 10  => "Low",       // Retail trader
            < 50  => "Medium",    // Active trader
            < 100 => "High",      // Institutional/Pro
            _     => "Critical"   // Whale/High Exposure
        };
}