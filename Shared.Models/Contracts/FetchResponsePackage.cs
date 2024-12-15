namespace Shared.Models.Contracts;

public class FetchResponsePackage
{
    public string? packageName { get; set; }
    public double noOfHours { get; set; }
    public double pricePerHour { get; set; }
    public double discountPercentage { get; set; } = 0;
    public double discountAmount { get; set; } = 0;
    public bool isDiscount { get; set; } = false;
}
