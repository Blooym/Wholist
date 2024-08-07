using System.IO;
using System.Runtime.CompilerServices;

namespace Wholist.Common
{
    /// <summary>
    ///     A wrapper around <see cref="PluginLog" /> that provides a nicer format.
    /// </summary>
    internal static class BetterLog
    {
        /// <summary>
        ///     Formats a log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string Format(string message, string? caller, string? file) => $"<{Path.GetFileName(file)}::{caller}>: {message}";

        /// <inheritdoc cref="PluginLog.Verbose(string, object[])" />
        internal static void Verbose(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => Services.PluginLog.Verbose(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Debug(string, object[])" />
        internal static void Debug(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => Services.PluginLog.Debug(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Information(string, object[])" />
        internal static void Information(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => Services.PluginLog.Information(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Warning(string, object[])" />
        internal static void Warning(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => Services.PluginLog.Warning(Format(message, caller, file));

        /// <inheritdoc cref="PluginLog.Error(string, object[])" />
        internal static void Error(string message, [CallerMemberName] string? caller = null, [CallerFilePath] string? file = null) => Services.PluginLog.Error(Format(message, caller, file));
    }
}
