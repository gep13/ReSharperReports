///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isLocalBuild        = !AppVeyor.IsRunningOnAppVeyor;
var isPullRequest       = AppVeyor.Environment.PullRequest.IsPullRequest;
var isDevelopBranch     = AppVeyor.Environment.Repository.Branch == "develop";
var isTag               = AppVeyor.Environment.Repository.Tag.IsTag;
var solution            = "./Source/Cake.Gitter.sln";
var solutionPath        = "./Source/Cake.Gitter";
var sourcePath          = "./Source";
var binDir              = "./Source/Cake.Gitter/bin/" + configuration;
var buildArtifacts      = "./BuildArtifacts";
var version             = "0.1.0";
var semVersion          = "0.1.0";

var assemblyInfo        = new AssemblyInfoSettings {
                                Title                   = "Cake.Gitter",
                                Description             = "Cake Gitter AddIn",
                                Product                 = "Cake.Gitter",
                                Company                 = "gep13",
                                Version                 = version,
                                FileVersion             = version,
                                InformationalVersion    = semVersion,
                                Copyright               = string.Format("Copyright © gep13 {0} - Present", DateTime.Now.Year),
                                CLSCompliant            = true
                            };
var nuGetPackSettings   = new NuGetPackSettings {
                                Id                      = assemblyInfo.Product,
                                Version                 = assemblyInfo.InformationalVersion,
                                Title                   = assemblyInfo.Title,
                                Authors                 = new[] {assemblyInfo.Company},
                                Owners                  = new[] {assemblyInfo.Company},
                                Description             = assemblyInfo.Description,
                                Summary                 = "Cake AddIn that extends Cake with Gitter messaging features",
                                ProjectUrl              = new Uri("https://github.com/gep13/Cake.Gitter/"),
                                LicenseUrl              = new Uri("https://github.com/gep13/Cake.Gitter/blob/master/LICENSE"),
                                Copyright               = assemblyInfo.Copyright,
                                ReleaseNotes            = new List<string>() { "https://github.com/gep13/Cake.Gitter/releases" },
                                Tags                    = new [] {"Cake", "Script", "Build", "Gitter"},
                                RequireLicenseAcceptance= false,
                                Symbols                 = false,
                                NoPackageAnalysis       = true,
                                Files                   = new [] {
                                                                    new NuSpecContent {Source = "Cake.Gitter.dll"},
                                                                    new NuSpecContent {Source = "Cake.Gitter.pdb"},
                                                                    new NuSpecContent {Source = "Cake.Gitter.xml"}
                                                                 },
                                BasePath                = binDir,
                                OutputDirectory         = buildArtifacts
                            };

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(() =>
{
    // Executed BEFORE the first task.
    Information("Running tasks...");
});

Teardown(() =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    Information("Cleaning {0}", solutionPath);
    CleanDirectories(solutionPath + "/**/bin/" + configuration);
    CleanDirectories(solutionPath + "/**/obj/" + configuration);

	Information("Cleaning BuildArtifacts");
	CleanDirectories(buildArtifacts);
});

Task("Restore")
    .Does(() =>
{
    // Restore all NuGet packages.
    Information("Restoring {0}...", solution);
    NuGetRestore(solution);
});

Task("SolutionInfo")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var file = "./Source/SolutionInfo.cs";
    CreateAssemblyInfo(file, assemblyInfo);
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("SolutionInfo")
    .IsDependentOn("DupFinder")
	.IsDependentOn("InspectCode")
    .Does(() =>
{
    Information("Building {0}", solution);
    MSBuild(solution, settings =>
        settings.SetPlatformTarget(PlatformTarget.MSIL)
            .WithProperty("TreatWarningsAsErrors","true")
            .WithTarget("Build")
            .SetConfiguration(configuration));
});

Task("DupFinder")
	.IsDependentOn("Create-BuildArtifacts-Directory")
    .Does(() =>
{
    // Run ReSharper's DupFinder
    DupFinder(solution, new DupFinderSettings() {
      ShowStats = true,
      ShowText = true,
      OutputFile = buildArtifacts + "/_ReSharperReports/dupfinder.xml",
      ExcludePattern = new string[] { MakeAbsolute(File("./Source/Cake.Gitter/Include_T4Include.cs")).ToString() },
      });
});

Task("InspectCode")
	.IsDependentOn("Create-BuildArtifacts-Directory")
    .Does(() =>
{
    // Run ReSharper's InspectCode
    InspectCode(solution, new InspectCodeSettings() {
      SolutionWideAnalysis = true,
	  Profile = sourcePath + "/Cake.Gitter.sln.DotSettings",
      OutputFile = buildArtifacts + "/_ReSharperReports/inspectcode.xml",
      });
});

Task("Create-BuildArtifacts-Directory")
	.Does(() =>
{
    if (!DirectoryExists(buildArtifacts))
    {
        CreateDirectory(buildArtifacts);
    }
});

Task("Create-NuGet-Package")
    .IsDependentOn("Build")
	.IsDependentOn("Create-BuildArtifacts-Directory")
    .Does(() =>
{
    NuGetPack(nuGetPackSettings);
});

Task("Publish-Nuget-Package")
    .IsDependentOn("Create-NuGet-Package")
    .WithCriteria(() => !isLocalBuild)
    .WithCriteria(() => !isPullRequest)
    .Does(() =>
{
    // Resolve the API key.
	var apiKey = EnvironmentVariable("MYGET_DEVELOP_API_KEY");
	if(!isDevelopBranch)
	{
		apiKey = EnvironmentVariable("MYGET_MASTER_API_KEY");
	}

	if(isTag) {
		apiKey = EnvironmentVariable("NUGET_API_KEY");
	}

    if(string.IsNullOrEmpty(apiKey)) {
        throw new InvalidOperationException("Could not resolve MyGet/Nuget API key.");
    }

    var source = EnvironmentVariable("MYGET_DEVELOP_SOURCE");
	if(!isDevelopBranch)
	{
		source = EnvironmentVariable("MYGET_MASTER_SOURCE");
	}

	if(isTag) {
		source = EnvironmentVariable("NUGET_SOURCE");
	}

    if(string.IsNullOrEmpty(source)) {
        throw new InvalidOperationException("Could not resolve MyGet/Nuget source.");
    }

    // Get the path to the package.
    var package = buildArtifacts + "/Cake.Gitter." + semVersion + ".nupkg";

    // Push the package.
    NuGetPush(package, new NuGetPushSettings {
        Source = source,
        ApiKey = apiKey
    });
});

Task("Default")
    .IsDependentOn("Create-NuGet-Package");

Task("AppVeyor")
    .IsDependentOn("Publish-Nuget-Package");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
