using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.Decompiler.TypeSystem.Implementation;

namespace WebApiForHikka.SharedFunction.Extensions;

public static class NullabilityInfoContextExtensions
{
    public static NullabilityInfo SubClassCreate(this NullabilityInfoContext context,
        NullabilityInfo nullability,
        Type subClass)
    {
        return context.SubClassCreate(nullability.Type, nullability.GenericTypeArguments,
            nullability.WriteState == NullabilityState.Nullable, nullability.ElementType, subClass);
    }

    public static NullabilityInfo SubClassCreate(this NullabilityInfoContext context,
        Type type,
        NullabilityInfo[] genericTypeArguments,
        bool isNullable,
        NullabilityInfo? elementType,
        Type subClass)
    {
        var typeSubClass =
            type.GetSubclassType(subClass) ??
            throw new ArgumentException("SubClass not found", nameof(subClass));

        if (type == typeSubClass)
            return ConstructNullabilityInfo(
                type,
                isNullable ? NullabilityState.Nullable : NullabilityState.NotNull,
                isNullable ? NullabilityState.Nullable : NullabilityState.NotNull,
                elementType,
                genericTypeArguments
            );

        var nullabilityInfo = context.SubClassCreate(type, subClass);

        if (!type.IsGenericType) return nullabilityInfo;

        var typeDefinition = type.GetGenericTypeDefinition();
        var genericArguments = typeDefinition.GetGenericArguments();
        var genericArgs = new NullabilityInfo?[genericArguments.Length];
        var definitionSubClass = subClass.IsInterface
            ? typeDefinition.GetInterfaces()[
                Array.IndexOf(type.GetInterfaces(), typeSubClass)]
            : typeDefinition.GetSubclassType(subClass.GetGenericTypeDefinition())!;
        MergeGenericNullability(genericTypeArguments, typeDefinition,
            definitionSubClass, ref genericArgs);
        var nullableContext = type.GetCustomAttribute<NullableContextAttribute>()?.Flag ?? 2;
        for (var i = 0; i < genericArgs.Length; i++)
        {
            var genericArg = nullabilityInfo.GenericTypeArguments[i];
            if (!genericArg.Type.IsGenericParameter) continue;
            var genericNullableAttribute = genericArg.Type
                .GetCustomAttribute<NullableAttribute>()?.NullableFlags[0] ?? nullableContext;
            genericArg = ConstructNullabilityInfo(
                genericArg.Type,
                genericNullableAttribute == 1
                    ? NullabilityState.NotNull
                    : NullabilityState.Nullable,
                genericNullableAttribute == 1
                    ? NullabilityState.NotNull
                    : NullabilityState.Nullable,
                genericArg.ElementType,
                genericArg.GenericTypeArguments
            );
            nullabilityInfo.GenericTypeArguments[i] = genericArgs[i] ?? genericArg;
        }

        return nullabilityInfo;
    }

    public static NullabilityInfo SubClassCreate(this NullabilityInfoContext context, Type type,
        Type subClass)
    {
        var typeSubClass =
            type.GetSubclassType(subClass) ??
            throw new ArgumentException("SubClass not found", nameof(subClass));
        var nullableContext = type.GetCustomAttribute<NullableContextAttribute>()?.Flag ?? 2;
        if (type == typeSubClass)
        {
            byte[] nullableArgs =
            [
                1,
                ..type.IsGenericType
                    ? type.GetGenericArguments().SelectMany(a =>
                        a.GetCustomAttribute<NullableAttribute>()?.NullableFlags ??
                        [nullableContext])
                    : []
            ];
            return context.GetNullabilityInfo(type, nullableArgs);
        }

        if (typeSubClass.IsGenericParameter)
            return context.GetNullabilityInfo(typeSubClass,
                typeSubClass.GetCustomAttribute<NullableAttribute>()?.NullableFlags ??
                [nullableContext]);

        if (!typeSubClass.IsGenericType) return context.GetNullabilityInfo(typeSubClass, [1]);

        if (subClass.IsInterface) return context.GetInterfaceNullabilityInfo(type, typeSubClass);

        var genericArgs = new NullabilityInfo?[subClass.GetGenericArguments().Length];
        for (var targetType = type;
             targetType != null && targetType.BaseType != typeSubClass;
             targetType = targetType.BaseType)
        {
            var baseClass = targetType.BaseType;
            if (baseClass?.IsGenericType != true) continue;
            var baseDefinition = baseClass.GetGenericTypeDefinition();
            var baseSubClass = baseDefinition.GetSubclassType(subClass.GetGenericTypeDefinition())!;
            context.MergeGenericNullability(type, baseSubClass, ref genericArgs);
        }

        for (var targetType = type;
             targetType != null;
             targetType = targetType.BaseType)
        {
            var baseClass = targetType.BaseType;
            if (baseClass != typeSubClass) continue;
            var nullableFlags = targetType.GetCustomAttribute<NullableAttribute>()?.NullableFlags ??
                                [1];
            nullableFlags[0] = 1;
            var nullabilityInfo = context.GetNullabilityInfo(baseClass, nullableFlags);
            nullableContext =
                targetType.GetCustomAttribute<NullableContextAttribute>()?.Flag ?? 2;
            for (var i = 0; i < genericArgs.Length; i++)
            {
                var genericArg = nullabilityInfo.GenericTypeArguments[i];
                if (genericArg.Type.IsGenericParameter)
                {
                    var genericNullableAttribute = genericArg.Type
                                                       .GetCustomAttribute<NullableAttribute>()
                                                       ?.NullableFlags[0] ??
                                                   nullableContext;
                    genericArg = ConstructNullabilityInfo(
                        genericArg.Type,
                        genericNullableAttribute == 1
                            ? NullabilityState.NotNull
                            : NullabilityState.Nullable,
                        genericNullableAttribute == 1
                            ? NullabilityState.NotNull
                            : NullabilityState.Nullable,
                        genericArg.ElementType,
                        genericArg.GenericTypeArguments
                    );
                }

                nullabilityInfo.GenericTypeArguments[i] = genericArgs[i] ?? genericArg;
            }

            return ConstructNullabilityInfo(
                nullabilityInfo.Type,
                nullabilityInfo.ReadState,
                nullabilityInfo.WriteState,
                nullabilityInfo.ElementType,
                genericArgs!
            );
        }

        throw new ArgumentException("SubClass not found", nameof(subClass));
    }

    #region Private

    private static readonly ConstructorInfo NullabilityInfoConstructor =
        typeof(NullabilityInfo).GetConstructors(
            BindingFlags.NonPublic | BindingFlags.Instance)[0];

    private static NullabilityInfo ConstructNullabilityInfo(Type type, NullabilityState readState,
        NullabilityState writeState, NullabilityInfo? elementType,
        NullabilityInfo[] genericTypeArguments)
    {
        return (NullabilityInfo)NullabilityInfoConstructor.Invoke([
            type,
            readState,
            writeState,
            elementType,
            genericTypeArguments
        ]);
    }

    private static NullabilityInfo GetNullabilityInfo(this NullabilityInfoContext context,
        Type type, byte[] nullableArgs)
    {
        var getNullabilityInfo = typeof(NullabilityInfoContext)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .First(m =>
                m.Name == "GetNullabilityInfo" && m.GetParameters().Length == 4);
        var nullableAttributeStateParserType =
            getNullabilityInfo.GetParameters()[2];

        var nullableAttributeStateParser = Activator.CreateInstance(
            nullableAttributeStateParserType.ParameterType,
            new ReadOnlyCollection<CustomAttributeTypedArgument>(
                nullableArgs.Select(f =>
                    new CustomAttributeTypedArgument(f.GetType(), f)).ToList()));

        var nullabilityInfo = getNullabilityInfo.Invoke(context,
            [null, type, nullableAttributeStateParser, 0]);
        return (NullabilityInfo)nullabilityInfo!;
    }

    private static void ParametrizedTypeToString(ParameterizedType parameterizedType,
        in Type[] genericArguments, ref StringBuilder stringBuilder)
    {
        stringBuilder.Append(parameterizedType.GenericType.ReflectionName + "[");
        for (var i = 0; i < parameterizedType.TypeArguments.Count; i++)
        {
            switch (parameterizedType.TypeArguments[i])
            {
                case ParameterizedType pt:
                    ParametrizedTypeToString(pt, in genericArguments, ref stringBuilder);
                    break;
                case DummyTypeParameter dummyTypeParameter:
                    stringBuilder.Append(genericArguments[dummyTypeParameter.Index]);
                    break;
                default:
                    stringBuilder.Append(parameterizedType.TypeArguments[i].ReflectionName);
                    break;
            }

            if (i < parameterizedType.TypeArguments.Count - 1)
                stringBuilder.Append(',');
        }

        stringBuilder.Append(']');
    }

    private static string ParametrizedTypeToString(ParameterizedType parameterizedType,
        in Type[] genericArguments)
    {
        var stringBuilder = new StringBuilder();
        ParametrizedTypeToString(parameterizedType, in genericArguments, ref stringBuilder);
        return stringBuilder.ToString();
    }

    private static void MergeGenericNullability(NullabilityInfo[] nullabilityArguments,
        Type baseClass,
        Type baseSubClass,
        ref NullabilityInfo?[] genericArgs)
    {
        var baseClassDefinition =
            baseClass.IsGenericType ? baseClass.GetGenericTypeDefinition() : baseClass;
        var sameArgs = baseClassDefinition.GetGenericArguments()
            .Select((a, i) => new
            {
                Index = i,
                SubClassIndex = baseSubClass.GetGenericArguments()
                    .Select((sa, si) => new
                    {
                        Argument = sa,
                        Index = si
                    })
                    .FirstOrDefault(sa => sa.Argument == a)
                    ?.Index
            })
            .Where(a => a.SubClassIndex != null)
            .ToArray();
        if (sameArgs.Length == 0)
            return;
        foreach (var sameArg in sameArgs)
            genericArgs[sameArg.SubClassIndex!.Value] ??=
                nullabilityArguments[sameArg.Index];
    }

    private static void MergeGenericNullability(this NullabilityInfoContext context, Type type,
        Type baseSubClass,
        ref NullabilityInfo?[] genericArgs)
    {
        var baseClass = type.BaseType!;
        var nullableFlags = type.GetCustomAttribute<NullableAttribute>()?.NullableFlags ?? [1];
        nullableFlags[0] = 1;
        var nullableContext = type.GetCustomAttribute<NullableContextAttribute>()?.Flag ?? 2;
        var baseNullabilityInfo = context.GetNullabilityInfo(baseClass, nullableFlags);
        for (var i = 0; i < baseNullabilityInfo.GenericTypeArguments.Length; i++)
        {
            var genericArg = baseNullabilityInfo.GenericTypeArguments[i];
            if (!genericArg.Type.IsGenericParameter) continue;
            var genericNullableAttribute = genericArg.Type
                .GetCustomAttribute<NullableAttribute>()?.NullableFlags[0] ?? nullableContext;
            baseNullabilityInfo.GenericTypeArguments[i] = ConstructNullabilityInfo(
                genericArg.Type,
                genericNullableAttribute == 1
                    ? NullabilityState.NotNull
                    : NullabilityState.Nullable,
                genericNullableAttribute == 1
                    ? NullabilityState.NotNull
                    : NullabilityState.Nullable,
                genericArg.ElementType,
                genericArg.GenericTypeArguments
            );
        }

        MergeGenericNullability(baseNullabilityInfo.GenericTypeArguments, baseClass, baseSubClass,
            ref genericArgs);
    }

    private static NullabilityInfo GetInterfaceNullabilityInfo(this NullabilityInfoContext context,
        Type type, Type subClass, ref NullabilityInfo?[] genericArgs)
    {
        if (type.BaseType == null ||
            !Array.Exists(type.BaseType.GetInterfaces(),
                i => i == subClass))
        {
            var runtimeTypeDefinition =
                type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            var runtimeTypeGenericArguments = runtimeTypeDefinition.GetGenericArguments();
            var runtimeSubClass =
                runtimeTypeDefinition.GetInterfaces()[
                    Array.IndexOf(type.GetInterfaces(), subClass)];

            using var peReader = new PEReader(File.OpenRead(type.Assembly.Location));
            var mdReader = peReader.GetMetadataReader();

            var typeDefinitionHandle = mdReader.TypeDefinitions
                .First(t =>
                    MetadataTokens.GetToken(t) == runtimeTypeDefinition.MetadataToken);
            var typeDefinition = mdReader.GetTypeDefinition(typeDefinitionHandle);

            var interfaceImplementation = typeDefinition.GetInterfaceImplementations()
                .Select(mdReader.GetInterfaceImplementation)
                .First(i =>
                {
                    var interfaceTypeHandle = i.Interface;
                    var interfaceType =
                        mdReader.GetTypeSpecification((TypeSpecificationHandle)interfaceTypeHandle);
                    var signature = (ParameterizedType)
                        interfaceType.DecodeSignature(
                            MetadataExtensions.MinimalSignatureTypeProvider,
                            default);
                    var signatureString =
                        ParametrizedTypeToString(signature, runtimeTypeGenericArguments);
                    return signatureString == runtimeSubClass.ToString();
                });

            var attribute = interfaceImplementation.GetCustomAttributes()
                .Select(mdReader.GetCustomAttribute)
                .FirstOrDefault(a =>
                {
                    var ctorHandle = a.Constructor;

                    var attributeTypeHandle = ctorHandle.Kind switch
                    {
                        HandleKind.MethodDefinition => mdReader
                            .GetMethodDefinition((MethodDefinitionHandle)ctorHandle)
                            .GetDeclaringType(),
                        HandleKind.MemberReference => mdReader
                            .GetMemberReference((MemberReferenceHandle)ctorHandle).Parent,
                        _ => throw new InvalidOperationException()
                    };

                    var attributeTypeNameHandle = attributeTypeHandle.Kind switch
                    {
                        HandleKind.TypeDefinition => mdReader
                            .GetTypeDefinition((TypeDefinitionHandle)attributeTypeHandle).Name,
                        HandleKind.TypeReference => mdReader
                            .GetTypeReference((TypeReferenceHandle)attributeTypeHandle).Name,
                        _ => throw new InvalidOperationException()
                    };

                    var attributeTypeNamespaceHandle = attributeTypeHandle.Kind switch
                    {
                        HandleKind.TypeDefinition => mdReader
                            .GetTypeDefinition((TypeDefinitionHandle)attributeTypeHandle).Namespace,
                        HandleKind.TypeReference => mdReader
                            .GetTypeReference((TypeReferenceHandle)attributeTypeHandle).Namespace,
                        _ => throw new InvalidOperationException()
                    };

                    return mdReader.StringComparer.Equals(attributeTypeNamespaceHandle,
                               "System.Runtime.CompilerServices") &&
                           mdReader.StringComparer.Equals(attributeTypeNameHandle,
                               "NullableAttribute");
                });

            var nullableAttribute = ConvertToNullableAttribute(attribute);
            var nullabilityInfo =
                context.GetNullabilityInfo(subClass, nullableAttribute.NullableFlags);
            var nullableContext = type.GetCustomAttribute<NullableContextAttribute>()?.Flag ?? 2;
            for (var i = 0; i < genericArgs.Length; i++)
            {
                var genericArg = nullabilityInfo.GenericTypeArguments[i];
                if (genericArg.Type.IsGenericParameter)
                {
                    var genericNullableAttribute = genericArg.Type
                                                       .GetCustomAttribute<NullableAttribute>()
                                                       ?.NullableFlags[0] ??
                                                   nullableContext;
                    genericArg = ConstructNullabilityInfo(
                        genericArg.Type,
                        genericNullableAttribute == 1
                            ? NullabilityState.NotNull
                            : NullabilityState.Nullable,
                        genericNullableAttribute == 1
                            ? NullabilityState.NotNull
                            : NullabilityState.Nullable,
                        genericArg.ElementType,
                        genericArg.GenericTypeArguments
                    );
                }

                nullabilityInfo.GenericTypeArguments[i] = genericArgs[i] ?? genericArg;
            }

            return nullabilityInfo;
        }

        var baseClass = type.BaseType;
        if (baseClass?.IsGenericType != true)
            return context.GetInterfaceNullabilityInfo(type.BaseType!, subClass, ref genericArgs);

        var baseDefinition = baseClass.GetGenericTypeDefinition();
        var baseSubClass = baseDefinition.GetInterfaces()[
            Array.IndexOf(baseClass.GetInterfaces(), subClass)];
        context.MergeGenericNullability(type, baseSubClass, ref genericArgs);

        return context.GetInterfaceNullabilityInfo(type.BaseType!, subClass, ref genericArgs);
    }

    private static NullabilityInfo GetInterfaceNullabilityInfo(this NullabilityInfoContext context,
        Type type, Type subClass)
    {
        var genericArgs = new NullabilityInfo?[subClass.GetGenericArguments().Length];
        return context.GetInterfaceNullabilityInfo(type, subClass, ref genericArgs);
    }

    private static NullableAttribute ConvertToNullableAttribute(CustomAttribute attribute)
    {
        var args = attribute.DecodeValue(MetadataExtensions.MinimalAttributeTypeProvider);
        var arg = (ImmutableArray<CustomAttributeTypedArgument<IType>>)args.FixedArguments[0]
            .Value!;
        var flags = arg.Select(a => a.Value).OfType<byte>().ToArray();
        flags[0] = 1;
        return new NullableAttribute(flags);
    }

    #endregion
}