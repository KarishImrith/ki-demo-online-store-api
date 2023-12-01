﻿namespace OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetById;

public class ShoppingCartItemGetByIdDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal CurrentSellingPrice { get; set; }

    public DateTimeOffset AddedDateTime { get; set; }
}
