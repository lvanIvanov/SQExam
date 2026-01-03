namespace System.Domain.Models;

public class Stock
{
    public string Symbol;
    public decimal Price;

    public Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        Price = price;
    }
}
