namespace OnlineStore.App;

public class AppOptions
{
    public string ODataRoutePrefix { get; set; }

    public int ODataMaxTop { get; set; }

    public int RateLimiterPermitLimit { get; set; }

    public int RateLimiterWindowSeconds { get; set; }

    public int RateLimiterQueueLimit { get; set; }
}
