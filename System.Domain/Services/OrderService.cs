using System.Domain.Models;
using System.Domain.Services;

namespace System.Domain.Services;

public class OrderService
{
    // SMELL: Hardcoded credentials/magic string
    private const string AdminRole = "ADMIN";

    private readonly INotificationService? _notificationService;

    // Constructor for Dependency Injection
    public OrderService(INotificationService? notificationService = null)
    {
        _notificationService = notificationService;
    }

    public void ExecuteOrder(Order order, Trader trader)
    {
        // SMELL: Nested logic increases cognitive complexity
        if (order.Status == OrderStatus.Created)
        {
            if (trader.Balance >= (order.Stock.Price * order.Quantity))
            {
                try
                {
                    order.Place(); // Internal logic to change status to Executed

                    if (order.Status == OrderStatus.Executed)
                    {
                        _notificationService?.NotifyTrader("Order executed successfully");
                    }
                }
                catch
                {
                    // BUG: Empty catch block silences errors (intentional for Static Analysis)
                }
            }
        }
    }

    // SECURITY: SQL injection vulnerability (intentional)
    public string GetOrdersBySymbolQuery(string symbolInput)
    {
        // Direct string concatenation is a major security flaw
        string query = "SELECT * FROM Orders WHERE Symbol = '" + symbolInput + "'";
        return query;
    }

    // Cyclomatic Complexity Demonstration: CC = 6
    // Calculation: 1 (base) + 5 decision points (if/else branches)
    public int DetermineOrderRiskLevel(Order order)
    {
        if (order.Quantity <= 0)
            return 99; // Error/Critical
        else if (order.Quantity < 10)
            return 1;  // Low
        else if (order.Quantity < 50)
            return 2;  // Medium
        else if (order.Quantity < 100)
            return 3;  // High
        else
            return 4;  // Institutional
    }
}