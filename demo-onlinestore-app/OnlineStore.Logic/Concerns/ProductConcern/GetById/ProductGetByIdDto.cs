namespace OnlineStore.Logic.Concerns.ProductConcern.GetById;

public class ProductGetByIdDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal CurrentSellingPrice { get; set; }
}
