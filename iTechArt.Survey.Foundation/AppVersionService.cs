using System;
using System.Reflection;

namespace iTechArt.Survey.Foundation
{
    public class AppVersionService: IAppVersionService
    {
        public string Version => (Assembly.GetEntryAssembly() ?? throw new InvalidOperationException()).GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
