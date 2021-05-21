using System;
using System.Reflection;

namespace iTechArt.Common
{
    public class AssemblyHelpers
    {
        public static string GetVersion()
        {
            return (Assembly.GetEntryAssembly() ?? throw new InvalidOperationException())
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        }
    }
}
