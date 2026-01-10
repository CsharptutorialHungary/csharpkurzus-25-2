
using System.Reflection;
namespace RentalApp.Core;

internal class ReflectionHelper
{

    public static TAttribute? GetAttribute<TAttribute>(Type type) where TAttribute : Attribute => type.GetCustomAttribute<TAttribute>();

    public static Type? GetImplementingClassAsRuntimeType<TInterface>()
    {
        Type interfaceType = typeof(TInterface);
        Assembly assembly = interfaceType.Assembly;
        IEnumerable<Type>? types = assembly.GetTypes();
        return types
            .Where(type => type.IsAssignableTo(interfaceType) && type.IsClass && type.IsAbstract is false)
            .First();
    }


}
