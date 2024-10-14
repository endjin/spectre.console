namespace Spectre.Console.Cli;

internal static class CommandPropertyBinder
{
#if !NETSTANDARD2_0
    public static CommandSettings CreateSettings(CommandValueLookup lookup, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type settingsType, ITypeResolver resolver)
#else
    public static CommandSettings CreateSettings(CommandValueLookup lookup, Type settingsType, ITypeResolver resolver)
#endif
    {
        var settings = CreateSettings(resolver, settingsType);

        foreach (var (parameter, value) in lookup)
        {
            if (value != default)
            {
                parameter.Property.SetValue(settings, value);
            }
        }

        // Validate the settings.
        var validationResult = settings.Validate();
        if (!validationResult.Successful)
        {
            throw CommandRuntimeException.ValidationFailed(validationResult);
        }

        return settings;
    }

#if !NETSTANDARD2_0
    private static CommandSettings CreateSettings(ITypeResolver resolver, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] Type settingsType)
#else
    private static CommandSettings CreateSettings(ITypeResolver resolver, Type settingsType)
#endif
    {
        if (resolver.Resolve(settingsType) is CommandSettings settings)
        {
            return settings;
        }

        if (Activator.CreateInstance(settingsType) is CommandSettings instance)
        {
            return instance;
        }

        throw CommandParseException.CouldNotCreateSettings(settingsType);
    }
}