namespace System.Domain.Models;

public class Trader
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
    
    
    public Trader(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }
}