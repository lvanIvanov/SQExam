using System.Security.Cryptography;

namespace System.Domain.Models;

public class Order
{
    public string Id { get; set; }
    public Stock Stock { get; set; }
    public int Quantity { get; set; }
    public OrderType Type { get; set; }
    public OrderStatus Status { get; set; } 
    
    public string OrderPriority { get; set; } = "Normal";

    public Order(Stock stock, int quantity, OrderType type)
    {
        Id = RandomNumberGenerator.GetInt32(1000, 99999).ToString();
        Stock = stock;
        Quantity = quantity;
        Type = type;
        Status = OrderStatus.Created;
    }
    
    public void Place()
    {
        if (Quantity > 0 && Quantity <= 1000)
        {
            Status = OrderStatus.Executed;
        }
        else
        {
            Reject();
        }
    }
    
    public void Reject()
    {
        Status = OrderStatus.Rejected;
    }
}