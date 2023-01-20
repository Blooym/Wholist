using System.IO;
using System.Runtime.CompilerServices;
using Dalamud.Logging;

namespace Wholist
{
    /// <summary>
    ///     A wrapper around <see cref="PluginLog"/> that provides a nicer format.
    /// </summary>
    public static class BetterLog
    {
        /// <summary>
        ///     Formats a log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string Format(string message, string? caller, string? file) => $"<{Path.GetFileName(file)}::{caller}>: {message}";

        /// <inheritdoc cref="PluginLog.Verbose(string, object[])"/>
        public static void Verbose(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => PluginLog.Verbose(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Debug(string, object[])"/>
        public static void Debug(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => PluginLog.Debug(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Information(string, object[])"/>
        public static void Information(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => PluginLog.Information(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Warning(string, object[])"/>
        public static void Warning(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => PluginLog.Warning(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Error(string, object[])"/>
        public static void Error(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => PluginLog.Error(Format(message, caller, file));
    }
}
