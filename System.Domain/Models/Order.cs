namespace System.Domain.Models;

public class Order
{
    public int Id;
    public Stock Stock;
    public int Quantity;
    public OrderType Type;
    public OrderStatus Status;

    public Order(Stock stock, int quantity, OrderType type)
    {
        Id = new Random().Next(1, 100000); // weak RNG (intentional)
        Stock = stock;
        Quantity = quantity;
        Type = type;
        Status = OrderStatus.Created;
    }

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
            Status = OrderStatus.Rejected;
        }
    }
}
