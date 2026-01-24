namespace CleanFirmeza.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderPlaced { get; set; }
    public DateTime OrderUpdated { get; set; }
    public string OrderStatus { get; set; }
    public bool IsActive { get; set; }
}