using System;
using System.Reflection;

namespace Wholist.Base
{
    internal static class Constants
    {
        /// <summary>
        ///     The plugin's name.
        /// </summary>
        internal const string Name = "Wholist";

        /// <summary>
        ///     The plugin's version.
        /// </summary>
        internal static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0, 0, 0);

        /// <summary>
        ///     The plugin's build git hash.
        /// </summary>
        internal static readonly string GitCommitHash = Assembly.GetExecutingAssembly().GetCustomAttribute<GitHashAttribute>()?.Value ?? "Unknown";

        /// <summary>
        ///     The plugin's last git commit date.
        /// </summary>
        internal static readonly DateTime GitCommitDate = DateTime.TryParse(Assembly.GetExecutingAssembly().GetCustomAttribute<GitCommitDateAttribute>()?.Value, out var date) ? date : DateTime.MinValue;

        /// <summary>
        ///     The plugin's git branch.
        /// </summary>
        internal static readonly string GitBranch = Assembly.GetExecutingAssembly().GetCustomAttribute<GitBranchAttribute>()?.Value ?? "Unknown";
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    internal sealed class GitHashAttribute : Attribute
    {
        public string Value { get; set; }
        public GitHashAttribute(string value) => this.Value = value;
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    internal sealed class GitCommitDateAttribute : Attribute
    {
        public string Value { get; set; }
        public GitCommitDateAttribute(string value) => this.Value = value;
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    internal sealed class GitBranchAttribute : Attribute
    {
        public string Value { get; set; }
        public GitBranchAttribute(string value) => this.Value = value;
    }
}
