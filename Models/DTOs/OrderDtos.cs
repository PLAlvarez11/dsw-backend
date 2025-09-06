public class OrderCreateDto
{
    public int PersonId { get; set; }
    public List<OrderDetailCreateDto> Details { get; set; } = new();
}

public class OrderDetailCreateDto
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

public class OrderReadDto
{
    public int Id { get; set; }
    public string PersonName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetailReadDto> Details { get; set; } = new();
}

public class OrderDetailReadDto
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}
