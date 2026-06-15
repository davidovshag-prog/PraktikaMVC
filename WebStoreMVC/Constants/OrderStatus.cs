namespace WebStoreMVC.Constants;

public static class OrderStatuses
{
    public const string New = "Нове";
    public const string Processing = "В обробці";
    public const string Confirmed = "Підтверджено";
    public const string Shipped = "Відправлено";
    public const string Delivered = "Доставлено";
    public const string Cancelled = "Скасовано";

    public static string[] All => new[]
    {
        New,
        Processing,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    };
}
