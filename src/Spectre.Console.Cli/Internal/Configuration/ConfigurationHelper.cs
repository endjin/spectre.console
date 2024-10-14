namespace Spectre.Console.Cli;

internal static class ConfigurationHelper
{
#if !NETSTANDARD2_0
    public static Type? GetSettingsType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type commandType)
#else
    public static Type? GetSettingsType(Type commandType)
#endif
    {
        if (typeof(ICommand).GetTypeInfo().IsAssignableFrom(commandType) &&
            GetGenericTypeArguments(commandType, typeof(ICommand<>), out var result))
        {
            return result[0];
        }

        return null;
    }

#if !NETSTANDARD2_0
    private static bool GetGenericTypeArguments([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type? type, Type genericType, [NotNullWhen(true)] out Type[]? genericTypeArguments)
#else
    private static bool GetGenericTypeArguments(Type? type, Type genericType, [NotNullWhen(true)] out Type[]? genericTypeArguments)
#endif
    {
        while (type != null)
        {
            foreach (var @interface in type.GetTypeInfo().GetInterfaces())
            {
                if (!@interface.GetTypeInfo().IsGenericType || @interface.GetGenericTypeDefinition() != genericType)
                {
                    continue;
                }

                genericTypeArguments = @interface.GenericTypeArguments;
                return true;
            }

            type = type.GetTypeInfo().BaseType;
        }

        genericTypeArguments = null;
        return false;
    }
}