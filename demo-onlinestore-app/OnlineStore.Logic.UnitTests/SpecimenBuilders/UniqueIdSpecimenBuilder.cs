using AutoFixture.Kernel;
using System.Reflection;

namespace OnlineStore.Logic.UnitTests.SpecimenBuilders;

public class UniqueIdSpecimenBuilder : ISpecimenBuilder
{
    private int _currentId = 0;

    public object Create(object request, ISpecimenContext context)
    {
        var pi = request as PropertyInfo;
        if (pi != null && pi.Name == "Id" && pi.PropertyType == typeof(long))
        {
            return ++_currentId;
        }

        return new NoSpecimen();
    }
}
