using System;

namespace Core.Entities.OrderAggregate;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public required string BuyerEmail { get; set; }
    public ShippingAddress ShippingAddress { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public PaymentSummary PaymentSummary { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = []; // Then I've used an I read only list for the order item here. That is going to have to be a list, because we do need to add our order items into lists in order to return them based on eagerly loading the entities.
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public required string PaymentIntentId { get; set; }
    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }
}
