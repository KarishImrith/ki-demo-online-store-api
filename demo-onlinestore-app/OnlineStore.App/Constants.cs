namespace OnlineStore.App;

public static class Constants
{
    public static class SecretKeyConstants
    {
        public const string SwaggerContactName = "Swagger:Contact:Name";
        public const string SwaggerContactEmailAddress = "Swagger:Contact:EmailAddress";
    }

    public static class SwaggerConstants
    {
        public const string DocumentDescription = "An ASP.NET Core Web API for managing an online store.";
        public const string DocumentTitle = $"{nameof(OnlineStore)} API";
        public const string EndpointUrl = $"/swagger/{Version}/swagger.json";
        public const string SecurityDefinitionOAuth2 = "oauth2";
        public const string Version = "v1";
    }
}
