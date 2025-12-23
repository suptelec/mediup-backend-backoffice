using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Utils;
public static class Check
{
    public static void NotNull<T>(T? val, string parameterName)
    {
        if (val == null)
        {
            throw new ArgumentNullException(parameterName, "The provided value cannot be null");
        }
    }

    public static void NotEmpty(string? val, string parameterName)
    {
        NotNull(val, parameterName);
        if (val!.Trim().Length == 0)
        {
            throw new ArgumentException("You need to provide a valid value", parameterName);
        }
    }

    public static void NotEmpty(long? val, string parameterName)
    {
        NotNull(val, parameterName);
        if (val <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, val, "You need to provide a valid value");
        }
    }

    public static void NotNull<T>(IReadOnlyCollection<T>? values, string parameterName)
    {
        if (values == null)
        {
            throw new ArgumentNullException(parameterName, "The provided value cannot be null");
        }
    }

    public static void NotEmpty<T>(IReadOnlyCollection<T>? values, string parameterName)
    {
        NotNull(values, parameterName);
        if (!values!.Any())
        {
            throw new ArgumentOutOfRangeException(parameterName, values, "You need to provide at least one value");
        }
    }

    public static void GreaterThanOrEqualTo(long val, string parameterName, int min = 0)
    {
        if (val < min)
        {
            throw new ArgumentOutOfRangeException(
                parameterName,
                val,
                $"The provided value = {val} must be greater than or equal to = {min}");
        }
    }

    public static void FileExists(string? path, string parameterName)
    {
        NotEmpty(path, parameterName);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File = {path} does not exist", path);
        }
    }

    public static void Url(string? val, string parameterName)
    {
        NotEmpty(val, parameterName);
        if (!Uri.TryCreate(val, UriKind.Absolute, out _))
        {
            throw new ArgumentOutOfRangeException(parameterName, val, "The provided url is not valid");
        }
    }

    public static bool IsEmpty(long val)
        => val <= 0;

    public static void NotEmpty(Stream? val, string parameterName)
    {
        NotNull(val, parameterName);
        if (val!.Length == 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, val, "You need to provide a valid value");
        }
    }

    public static void Host(string? val, string parameterName)
    {
        NotEmpty(val, parameterName);
        if (Uri.CheckHostName(val) == UriHostNameType.Unknown)
        {
            throw new ArgumentOutOfRangeException(parameterName, val, "You need to provide a valid host");
        }
    }

    public static void FileName(string? val, string parameterName)
    {
        NotEmpty(val, parameterName);
        if (string.IsNullOrEmpty(Path.GetExtension(val)) || val!.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            throw new ArgumentException("You need to provide a valid value", parameterName);
    }
}
