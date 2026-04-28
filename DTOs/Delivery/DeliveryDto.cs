namespace SupplySync.DTOs.Delivery
{
    public class CreateDeliveryItemRequestDto
    {
        public string Item { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }


    public class DeliveryItemDto
    {
        public string Item { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }


    public class CreateDeliveryRequestDto
    {
        public int POID { get; set; }
        public int VendorID { get; set; }
        public DateTime Date { get; set; }

        public List<DeliveryItemDto>? Items { get; set; }

        public string Status { get; set; } = "Shipped";

        // Optional flattened fields
        public string? Item { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateDeliveryRequestDto
    {
        public string Item { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class DeliveryResponseDto
    {
        public int DeliveryID { get; set; }
        public int POID { get; set; }
        public int VendorID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;

        public List<DeliveryItemDto>? Items { get; set; }
    }

    public class DeliveryListResponseDto
    {
        public List<DeliveryResponseDto> Deliveries { get; set; } = new();
        public int TotalCount { get; set; }
    }
}