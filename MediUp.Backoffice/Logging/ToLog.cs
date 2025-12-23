using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Logging;

public class ToLog
{
    private const string Separator = "_";

    private static readonly string[] ReservedSuffixes = new string[1] { "HostedService" };

    private static readonly Type[] LoggerTypes = new Type[3]
    {
        typeof(ILoggerFactory),
        typeof(ILogger),
        typeof(ILogger<>)
    };

    public string Source { get; } = string.Empty;


    public string LogFileName { get; } = string.Empty;


    public ToLog(Type type)
        : this(type, GenerateFilename(type.Name))
    {
    }

    public ToLog(Type type, string filename)
        : this(type.FullName, filename)
    {
    }

    public ToLog(string source, string filename)
        : this(filename)
    {
        Source = source;
    }

    private ToLog(string filename)
    {
        LogFileName = filename;
        if (!Path.HasExtension(LogFileName))
        {
            LogFileName += "_.txt";
        }
    }

    public static List<ToLog> From<TClass>()
    {
        Type? typeFromHandle = typeof(TClass);
        return From(assembly: typeFromHandle.Assembly, @namespace: typeFromHandle.Namespace);
    }

    public static List<ToLog> From(Type type)
    {
        Assembly assembly = type.Assembly;
        return From(type.Namespace, assembly);
    }

    public static List<ToLog> From(string @namespace, Assembly assembly)
    {
        string namespace2 = @namespace;
        return (from t in assembly.GetTypes()
                where IsMatch(namespace2, t)
                select t into type
                select new ToLog(type)).ToList();
    }

    public static bool IsMatch(string @namespace, Type type)
    {
        if ((object)type == null || !type.IsClass || type.IsAbstract || Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) != null || string.IsNullOrWhiteSpace(type.Namespace) || !type.Namespace.Contains(@namespace))
        {
            return false;
        }

        foreach (Type t in (from p in type.GetConstructors().SelectMany((ConstructorInfo ctor) => ctor.GetParameters())
                            select p.ParameterType).ToList())
        {
            if (LoggerTypes.Contains(t))
            {
                return true;
            }

            if (LoggerTypes.Any((Type lt) => lt.IsAssignableFrom(t)))
            {
                return true;
            }
        }

        return false;
    }

    public static string GenerateFilename(string typeName)
    {
        string[] reservedSuffixes = ReservedSuffixes;
        foreach (string text in reservedSuffixes)
        {
            if (typeName.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                string newValue = char.ToUpperInvariant(text[0]) + text.Substring(1).ToLowerInvariant();
                typeName = typeName.Replace(text, newValue, StringComparison.OrdinalIgnoreCase);
            }
        }

        string[] array = Regex.Split(typeName, "(?<!^)(?=[A-Z])");
        switch (array.Length)
        {
            case 0:
                throw new ArgumentOutOfRangeException(typeName, "The type name cannot be split");
            case 1:
                return array.First().ToLowerInvariant();
            default:
            {
                string text2 = array.Last().ToLowerInvariant();
                string text3 = string.Join("_", array.Take(array.Length - 1)).ToLowerInvariant();
                // Use a simple string array instead of inline array and private implementation details
                return string.Join("_", new[] { text2, text3 });
            }
        }
    }
}
