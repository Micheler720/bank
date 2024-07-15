namespace Bank.CreditCard.Worker.Domain;
public class CreditCard
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; }
    public string SecurityCode { get; set; }
    public decimal CreditLimit { get; set; }
}