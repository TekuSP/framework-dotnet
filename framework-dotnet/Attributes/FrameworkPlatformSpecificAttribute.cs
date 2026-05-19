using System;

using FrameworkDotnet.Enums;

namespace FrameworkDotnet.Attributes;

/// <summary>
/// Indicates that a public API is documented or intended for specific Framework platform families.
/// </summary>
/// <remarks>
/// The companion Roslyn analyzer reports a warning when an annotated API is used from code that is not already constrained to the same Framework platform family.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class FrameworkPlatformSpecificAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrameworkPlatformSpecificAttribute"/> class.
    /// </summary>
    /// <param name="platformFamilies">The Framework platform families associated with the annotated API.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="platformFamilies"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="platformFamilies"/> is empty.</exception>
    public FrameworkPlatformSpecificAttribute(params FrameworkPlatformFamily[] platformFamilies)
    {
        ArgumentNullException.ThrowIfNull(platformFamilies);

        if (platformFamilies.Length == 0)
        {
            throw new ArgumentException("At least one Framework platform family must be specified.", nameof(platformFamilies));
        }

        PlatformFamilies = (FrameworkPlatformFamily[])platformFamilies.Clone();
    }

    /// <summary>
    /// Gets the Framework platform families associated with the annotated API.
    /// </summary>
    public FrameworkPlatformFamily[] PlatformFamilies { get; }

    /// <summary>
    /// Gets or sets an optional explanatory message.
    /// </summary>
    public string? Message { get; set; }
}
