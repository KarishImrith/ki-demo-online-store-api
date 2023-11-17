using Serilog;

namespace OnlineStore.App.Helpers;

public static class Serilog
{
    public static void ConfigureSerilogSupport(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog(ConfigureLogger);
    }

    private static void ConfigureLogger(HostBuilderContext hostBuilderContext, LoggerConfiguration loggerConfiguration)
        => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
}
