using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(PrefLib.BuildInfo.Description)]
[assembly: AssemblyDescription(PrefLib.BuildInfo.Description)]
[assembly: AssemblyCompany(PrefLib.BuildInfo.Company)]
[assembly: AssemblyProduct(PrefLib.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + PrefLib.BuildInfo.Author)]
[assembly: AssemblyTrademark(PrefLib.BuildInfo.Company)]
[assembly: AssemblyVersion(PrefLib.BuildInfo.Version)]
[assembly: AssemblyFileVersion(PrefLib.BuildInfo.Version)]
[assembly: MelonInfo(typeof(PrefLib.PrefLib), PrefLib.BuildInfo.Name, PrefLib.BuildInfo.Version, PrefLib.BuildInfo.Author, PrefLib.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]