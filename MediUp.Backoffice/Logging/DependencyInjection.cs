using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Json;
using System;
using System.Drawing;
using System.IO;

namespace Logging;

public static class DependencyInjection
{
    public const string SimpleLoggingFormat = "({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}";

    public const string FullLoggingFormat = "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}";

    public const int TenMb = 10000000;

    private static void TryCreateLogFolder(string path)
    {
        if (Directory.Exists(path))
        {
            return;
        }

        try
        {
            Directory.CreateDirectory(path);
        }
        catch
        {
        }
    }

    public static Serilog.ILogger CreateBootstrapperLogger(ToLog? log = null, string? logsPath = null)
    {
        if (!string.IsNullOrWhiteSpace(logsPath))
        {
            TryCreateLogFolder(logsPath);
        }

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration().Enrich.WithThreadId().Enrich.FromLogContext().WriteTo.Debug(LogEventLevel.Verbose, "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}").WriteTo.Console(new JsonFormatter());
        if (!string.IsNullOrWhiteSpace(logsPath) && log != null)
        {
            loggerConfiguration.WriteTo.File(Path.Combine(logsPath, log.LogFileName), LogEventLevel.Verbose, "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}", null, 10000000L, null, buffered: false, shared: false, null, RollingInterval.Day, rollOnFileSizeLimit: true, 31);
        }

        return loggerConfiguration.CreateBootstrapLogger();
    }

    public static IHostBuilder ConfigureAppLogging(this IHostBuilder builder, string? logsPath = null, bool useJsonFormatOnFiles = false, bool useJsonFormatOnConsole = false, params ToLog[] logs)
    {
        string logsPath2 = logsPath;
        ToLog[] logs2 = logs;
        bool logToFile = !string.IsNullOrWhiteSpace(logsPath2);
        if (logToFile)
        {
            TryCreateLogFolder(logsPath2);
        }

        return builder.UseSerilog(delegate (HostBuilderContext context, IServiceProvider provider, LoggerConfiguration config)
        {
            SetDefaultConfig(config, provider, context.Configuration);
            ConfigureLogs(config, useJsonFormatOnFiles, useJsonFormatOnConsole, logToFile, logsPath2, logs2);
        }, preserveStaticLogger: true);
    }

    public static ILoggingBuilder ConfigureAppLogging(this ILoggingBuilder loggingBuilder, string? logsPath = null, bool useJsonFormatOnFiles = false, bool useJsonFormatOnConsole = false, params ToLog[] logs)
    {
        bool flag = !string.IsNullOrWhiteSpace(logsPath);
        if (flag)
        {
            TryCreateLogFolder(logsPath);
        }

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
        SetDefaultConfig(loggerConfiguration);
        ConfigureLogs(loggerConfiguration, useJsonFormatOnFiles, useJsonFormatOnConsole, flag, logsPath, logs);
        return loggingBuilder.AddSerilog(loggerConfiguration.CreateLogger());
    }

    public static ILoggingBuilder ConfigureAppLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration, string? logsPath = null, bool useJsonFormatOnFiles = false, bool useJsonFormatOnConsole = false, params ToLog[] logs)
    {
        bool flag = !string.IsNullOrWhiteSpace(logsPath);
        if (flag)
        {
            TryCreateLogFolder(logsPath);
        }

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
        SetDefaultConfig(loggerConfiguration, null, configuration);
        ConfigureLogs(loggerConfiguration, useJsonFormatOnFiles, useJsonFormatOnConsole, flag, logsPath, logs);
        return loggingBuilder.AddSerilog(loggerConfiguration.CreateLogger());
    }

    private static void SetDefaultConfig(LoggerConfiguration config, IServiceProvider? provider = null, IConfiguration? configuration = null)
    {
        config.Enrich.WithThreadId().Enrich.FromLogContext();
        if (provider != null)
        {
            config.ReadFrom.Services(provider);
        }

        if (configuration != null)
        {
            config.ReadFrom.Configuration(configuration);
        }
    }

    private static void ConfigureLogs(LoggerConfiguration config, bool useJsonFormatOnFiles, bool useJsonFormatOnConsole, bool logToFile, string? logsPath, params ToLog[] logs)
    {
        string logsPath2 = logsPath;
        foreach (ToLog log in logs)
        {
            string filename = log.LogFileName;
            config.WriteTo.Logger(delegate (LoggerConfiguration l)
            {
                LoggerConfiguration loggerConfiguration = l.Filter.ByIncludingOnly(Matching.FromSource(log.Source));
                loggerConfiguration.WriteTo.Debug(LogEventLevel.Verbose, "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}");
                if (useJsonFormatOnConsole)
                {
                    loggerConfiguration.WriteTo.Console(new JsonFormatter());
                }
                else
                {
                    loggerConfiguration.WriteTo.Console(LogEventLevel.Verbose, "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}");
                }

                if (logToFile)
                {
                    if (useJsonFormatOnFiles)
                    {
                        loggerConfiguration.WriteTo.File(new JsonFormatter(), Path.Combine(logsPath2, filename), LogEventLevel.Verbose, 10000000L, null, buffered: false, shared: false, null, RollingInterval.Day, rollOnFileSizeLimit: true, 31);
                    }
                    else
                    {
                        loggerConfiguration.WriteTo.File(Path.Combine(logsPath2, filename), LogEventLevel.Verbose, "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} ({ThreadId}) [{Level}] {Message:lj}{NewLine}{Exception}", null, 10000000L, null, buffered: false, shared: false, null, RollingInterval.Day, rollOnFileSizeLimit: true, 31);
                    }
                }
            });
        }
    }
}
