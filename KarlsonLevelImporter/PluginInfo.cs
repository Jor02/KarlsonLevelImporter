using KarlsonLevelImporter;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: AssemblyVersion(PluginInfo.VERSION)]
[assembly: AssemblyTitle(PluginInfo.NAME + " (" + PluginInfo.GUID + ")")]
[assembly: AssemblyProduct(PluginInfo.NAME)]
[assembly: MelonLoader.MelonInfo(typeof(Base), PluginInfo.NAME, PluginInfo.VERSION)]
[assembly: MelonLoader.MelonGame("Dani", "Karlson")]

namespace KarlsonLevelImporter
{
    internal static class PluginInfo
    {
        public const string GUID = "com.jor02.karlsonlevelimporter";
        public const string NAME = "Karlson Level Importer";
        public const string VERSION = "1.1";
    }
}
