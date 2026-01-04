namespace System.Domain.Models;

public class Order
{
    public string Id { get; set; }
    public Stock Stock { get; set; }
    public int Quantity { get; set; }
    public OrderType Type { get; set; }
    public OrderStatus Status { get; set; }

    // SMELL: Public field breaking encapsulation (Intentional)
    public string OrderPriority = "Normal";

    public Order(Stock stock, int quantity, OrderType type)
    {
        // SECURITY: Weak random number generator for ID (Intentional)
        Id = new Random().Next(1000, 99999).ToString();
        
        Stock = stock;
        Quantity = quantity;
        Type = type;
        Status = OrderStatus.Created;
    }

    // Cyclomatic Complexity: CC = 3
    // Calculation: 1 (base) + 2 decision points (if statements at line 30, 32)
    public void Place()
    {
        if (Quantity > 0)
        {
            if (Quantity <= 1000)
            {
                Status = OrderStatus.Executed;
            }
            else
            {
                Status = OrderStatus.Rejected;
            }
        }
        else
        {
            // Note: This branch is covered by CC = 3 logic
            Status = OrderStatus.Rejected;
        }
    }
}