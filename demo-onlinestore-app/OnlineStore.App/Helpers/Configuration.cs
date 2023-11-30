namespace OnlineStore.App.Helpers;

public static class Configuration
{
    public static TConfig GetConfig<TConfig>(this IConfiguration configuration)
        where TConfig : class, new()
        => configuration.GetSection(typeof(TConfig).Name).Get<TConfig>();
}
