namespace System.Domain.Models;

public class Stock
{
    public string Symbol { get; set; }
    public decimal Price { get; set; }

    public Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        Price = price;
    }
}