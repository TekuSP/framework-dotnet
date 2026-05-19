using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace FrameworkDotnet.Analyzers;

/// <summary>
/// Reports a warning when a Framework-model-specific API is used from an unannotated call site.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class FrameworkPlatformSpecificAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// The diagnostic identifier for platform-specific Framework API usage warnings.
    /// </summary>
    public const string DiagnosticId = "FD0001";

    private const string AttributeMetadataName = "FrameworkDotnet.Attributes.FrameworkPlatformSpecificAttribute";
    private const string MessagePropertyName = "Message";
    private const string GuidanceMessage = "Guard this usage by checking FrameworkSystem.GetPlatformFamily() or annotate the containing member with FrameworkPlatformSpecificAttribute.";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Framework model-specific API usage",
        "'{0}' is limited to {1}: {2}",
        "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "FrameworkDotnet APIs marked with FrameworkPlatformSpecificAttribute should only be used from code paths that are already constrained to the same Framework hardware family.");

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(
            compilationContext =>
            {
                INamedTypeSymbol? platformSpecificAttribute = compilationContext.Compilation.GetTypeByMetadataName(AttributeMetadataName);
                if (platformSpecificAttribute is null)
                {
                    return;
                }

                compilationContext.RegisterOperationAction(
                    operationContext => AnalyzeUsage(operationContext, ((IInvocationOperation)operationContext.Operation).TargetMethod, platformSpecificAttribute),
                    OperationKind.Invocation);

                compilationContext.RegisterOperationAction(
                    operationContext => AnalyzeUsage(operationContext, ((IPropertyReferenceOperation)operationContext.Operation).Property, platformSpecificAttribute),
                    OperationKind.PropertyReference);

                compilationContext.RegisterOperationAction(
                    operationContext => AnalyzeUsage(operationContext, ((IFieldReferenceOperation)operationContext.Operation).Field, platformSpecificAttribute),
                    OperationKind.FieldReference);
            });
    }

    private static void AnalyzeUsage(OperationAnalysisContext context, ISymbol referencedSymbol, INamedTypeSymbol platformSpecificAttribute)
    {
        AttributeData? attribute = GetPlatformSpecificAttribute(referencedSymbol, platformSpecificAttribute);
        if (attribute is null)
        {
            return;
        }

        ImmutableHashSet<string> targetFamilies = GetPlatformFamilies(attribute);
        if (targetFamilies.Count == 0)
        {
            return;
        }

        if (IsSuppressedByContainingContext(context.ContainingSymbol, targetFamilies, platformSpecificAttribute))
        {
            return;
        }

        string detailMessage = ComposeDetailMessage(attribute);
        string platformFamilyList = string.Join(", ", targetFamilies.OrderBy(static family => family, StringComparer.Ordinal));
        string symbolDisplayName = referencedSymbol.ToDisplayString(SymbolDisplayFormat.CSharpShortErrorMessageFormat);

        context.ReportDiagnostic(Diagnostic.Create(Rule, context.Operation.Syntax.GetLocation(), symbolDisplayName, platformFamilyList, detailMessage));
    }

    private static bool IsSuppressedByContainingContext(ISymbol? containingSymbol, ImmutableHashSet<string> targetFamilies, INamedTypeSymbol platformSpecificAttribute)
    {
        for (ISymbol? current = containingSymbol; current is not null; current = current.ContainingSymbol)
        {
            AttributeData? attribute = GetPlatformSpecificAttribute(current, platformSpecificAttribute);
            if (attribute is null)
            {
                continue;
            }

            ImmutableHashSet<string> containingFamilies = GetPlatformFamilies(attribute);
            if (containingFamilies.Count > 0 && containingFamilies.All(targetFamilies.Contains))
            {
                return true;
            }
        }

        return false;
    }

    private static AttributeData? GetPlatformSpecificAttribute(ISymbol symbol, INamedTypeSymbol platformSpecificAttribute)
    {
        AttributeData? directAttribute = symbol.GetAttributes().FirstOrDefault(
            attribute => SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, platformSpecificAttribute));
        if (directAttribute is not null)
        {
            return directAttribute;
        }

        return symbol switch
        {
            IMethodSymbol methodSymbol =>
                GetMethodPlatformSpecificAttribute(methodSymbol, platformSpecificAttribute),
            IPropertySymbol propertySymbol =>
                GetPropertyPlatformSpecificAttribute(propertySymbol, platformSpecificAttribute),
            _ => null,
        };
    }

    private static AttributeData? GetMethodPlatformSpecificAttribute(IMethodSymbol methodSymbol, INamedTypeSymbol platformSpecificAttribute)
    {
        if (methodSymbol.OverriddenMethod is not null)
        {
            AttributeData? overriddenAttribute = GetPlatformSpecificAttribute(methodSymbol.OverriddenMethod, platformSpecificAttribute);
            if (overriddenAttribute is not null)
            {
                return overriddenAttribute;
            }
        }

        foreach (IMethodSymbol implementedMethod in methodSymbol.ExplicitInterfaceImplementations)
        {
            AttributeData? interfaceAttribute = GetPlatformSpecificAttribute(implementedMethod, platformSpecificAttribute);
            if (interfaceAttribute is not null)
            {
                return interfaceAttribute;
            }
        }

        return GetInterfaceMemberPlatformSpecificAttribute(methodSymbol, platformSpecificAttribute);
    }

    private static AttributeData? GetPropertyPlatformSpecificAttribute(IPropertySymbol propertySymbol, INamedTypeSymbol platformSpecificAttribute)
    {
        if (propertySymbol.OverriddenProperty is not null)
        {
            AttributeData? overriddenAttribute = GetPlatformSpecificAttribute(propertySymbol.OverriddenProperty, platformSpecificAttribute);
            if (overriddenAttribute is not null)
            {
                return overriddenAttribute;
            }
        }

        foreach (IPropertySymbol implementedProperty in propertySymbol.ExplicitInterfaceImplementations)
        {
            AttributeData? interfaceAttribute = GetPlatformSpecificAttribute(implementedProperty, platformSpecificAttribute);
            if (interfaceAttribute is not null)
            {
                return interfaceAttribute;
            }
        }

        return GetInterfaceMemberPlatformSpecificAttribute(propertySymbol, platformSpecificAttribute);
    }

    private static AttributeData? GetInterfaceMemberPlatformSpecificAttribute(ISymbol symbol, INamedTypeSymbol platformSpecificAttribute)
    {
        INamedTypeSymbol? containingType = symbol.ContainingType;
        if (containingType is null)
        {
            return null;
        }

        foreach (INamedTypeSymbol interfaceType in containingType.AllInterfaces)
        {
            foreach (ISymbol interfaceMember in interfaceType.GetMembers(symbol.Name))
            {
                ISymbol? implementation = containingType.FindImplementationForInterfaceMember(interfaceMember);
                if (implementation is null)
                {
                    continue;
                }

                if (!SymbolEqualityComparer.Default.Equals(implementation.OriginalDefinition, symbol.OriginalDefinition))
                {
                    continue;
                }

                AttributeData? interfaceAttribute = GetPlatformSpecificAttribute(interfaceMember, platformSpecificAttribute);
                if (interfaceAttribute is not null)
                {
                    return interfaceAttribute;
                }
            }
        }

        return null;
    }

    private static ImmutableHashSet<string> GetPlatformFamilies(AttributeData attribute)
    {
        if (attribute.ConstructorArguments.Length == 0)
        {
            return ImmutableHashSet<string>.Empty;
        }

        TypedConstant platformFamiliesArgument = attribute.ConstructorArguments[0];
        if (platformFamiliesArgument.Kind != TypedConstantKind.Array)
        {
            return ImmutableHashSet<string>.Empty;
        }

        ImmutableHashSet<string>.Builder builder = ImmutableHashSet.CreateBuilder<string>(StringComparer.Ordinal);
        foreach (TypedConstant platformFamily in platformFamiliesArgument.Values)
        {
            string? name = GetEnumMemberName(platformFamily);
            if (!string.IsNullOrWhiteSpace(name))
            {
                builder.Add(name!);
            }
        }

        return builder.ToImmutable();
    }

    private static string ComposeDetailMessage(AttributeData attribute)
    {
        string? customMessage = null;

        foreach (KeyValuePair<string, TypedConstant> namedArgument in attribute.NamedArguments)
        {
            if (namedArgument.Key == MessagePropertyName)
            {
                customMessage = namedArgument.Value.Value as string;
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(customMessage))
        {
            return GuidanceMessage;
        }

        string trimmedMessage = customMessage!.Trim();
        if (!trimmedMessage.EndsWith(".", StringComparison.Ordinal))
        {
            trimmedMessage += ".";
        }

        return $"{trimmedMessage} {GuidanceMessage}";
    }

    private static string? GetEnumMemberName(TypedConstant typedConstant)
    {
        if (typedConstant.Type is not INamedTypeSymbol enumType || typedConstant.Value is null)
        {
            return null;
        }

        foreach (IFieldSymbol member in enumType.GetMembers().OfType<IFieldSymbol>())
        {
            if (!member.HasConstantValue)
            {
                continue;
            }

            if (Equals(member.ConstantValue, typedConstant.Value))
            {
                return member.Name;
            }
        }

        return null;
    }
}
