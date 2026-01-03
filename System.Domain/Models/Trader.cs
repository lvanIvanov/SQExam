namespace System.Domain.Models;

public class Trader
{
    public string Name { get; set; }
    public decimal Balance { get; set; }

    private int _internalRiskCalculation; // unused field (intentional)

    public Trader(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }
}
