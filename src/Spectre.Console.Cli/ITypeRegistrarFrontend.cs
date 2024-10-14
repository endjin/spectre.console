namespace Spectre.Console.Cli;

/// <summary>
/// Represents a user friendly frontend for a <see cref="ITypeRegistrar"/>.
/// </summary>
public interface ITypeRegistrarFrontend
{
    /// <summary>
    /// Registers the type with the type registrar as a singleton.
    /// </summary>
    /// <typeparam name="TService">The exposed service type.</typeparam>
    /// <typeparam name="TImplementation">The implementing type.</typeparam>
#if !NETSTANDARD2_0
    void Register<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
#else
    void Register<TService, TImplementation>()
#endif
        where TImplementation : TService;

    /// <summary>
    /// Registers the specified instance with the type registrar as a singleton.
    /// </summary>
    /// <typeparam name="TImplementation">The type of the instance.</typeparam>
    /// <param name="instance">The instance to register.</param>
    void RegisterInstance<TImplementation>(TImplementation instance);

    /// <summary>
    /// Registers the specified instance with the type registrar as a singleton.
    /// </summary>
    /// <typeparam name="TService">The exposed service type.</typeparam>
    /// <typeparam name="TImplementation"> implementing type.</typeparam>
    /// <param name="instance">The instance to register.</param>
    void RegisterInstance<TService, TImplementation>(TImplementation instance)
        where TImplementation : TService;
}