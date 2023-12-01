using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OnlineStore.App.Controllers;
using OnlineStore.App.Factories;
using OnlineStore.Logic.Concerns.ProductConcern.GetAll;
using OnlineStore.Logic.Concerns.ShoppingCartItemConcern.GetAll;

namespace OnlineStore.App.Helpers;

public static class OData
{
    public static IMvcBuilder AddODataSupport(this IMvcBuilder mvcBuilder, string routePrefix, int oDataMaxTop)
    {
        mvcBuilder
            .AddOData(options => options.AddRouteComponents(routePrefix, BuildEdmModel())
                .Select()
                .Filter()
                .OrderBy()
                .Count()
                .SetMaxTop(oDataMaxTop))
            .AddJsonOptions(configure => JsonSerializerOptionsFactory.ConfigureJsonSerializerOptions());

        return mvcBuilder;
    }

    private static IEdmModel BuildEdmModel()
    {
        var oDataConventionModelBuilder = new ODataConventionModelBuilder();
        oDataConventionModelBuilder.EnableLowerCamelCase();

        // Configure odata controllers here
        oDataConventionModelBuilder.EntitySet<ProductGetAllDto, ProductController>();
        oDataConventionModelBuilder.EntitySet<ShoppingCartItemGetAllDto, ShoppingCartItemController>();

        return oDataConventionModelBuilder.GetEdmModel();
    }

    private static EntitySetConfiguration<TDto> EntitySet<TDto, TController>(this ODataConventionModelBuilder oDataConventionModelBuilder)
        where TDto : class
        where TController : ControllerBase
        => oDataConventionModelBuilder.EntitySet<TDto>(GetCleanControllerName<TController>());

    private static string GetCleanControllerName<TController>()
        where TController : ControllerBase
        => typeof(TController).Name.Replace(nameof(Controller), string.Empty);

    private static OperationConfiguration ODataParameter<TParameter>(this OperationConfiguration operationConfiguration, string name)
    {
        operationConfiguration.Parameter<TParameter>(name);

        return operationConfiguration;
    }
}
