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
    
    public void DeductBalance(decimal amount)
    {
        if (amount > Balance) 
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }
}