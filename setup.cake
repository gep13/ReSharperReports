///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// PROJECT SPECIFIC VARIABLES
///////////////////////////////////////////////////////////////////////////////
var solution            = "./Source/ReSharperReports.sln";
var solutionPath        = "./Source/ReSharperReports";
var sourcePath          = "./Source";
var binDir              = "./Source/ReSharperReports/bin/" + configuration;
var buildArtifacts      = "./BuildArtifacts";
var title               = "ReSharperReports";
var description         = "Command line tool to allow generation of human readable ReSharper Reports";
var product             = "ReSharperReports";
var company             = "gep13";
var copyright           = string.Format("Copyright © gep13 and Contributors {0} - Present", DateTime.Now.Year);
var projectUrl          = new Uri("https://github.com/gep13/ReSharperReports/");
var licenseUrl          = new Uri("https://github.com/gep13/ReSharperReports/blob/master/LICENSE");
var releaseNotes        = new List<string>() { "https://github.com/gep13/ReSharperReports/releases" };
var tags                = new [] {"ReSharper", "DupFinder", "InspectCode", "Reports"};

var version             = "0.2.0";
var semVersion          = "0.2.0";

var assemblyInfo        = new AssemblyInfoSettings {
                                Title                   = title,
                                Description             = description,
                                Product                 = product,
                                Company                 = company,
                                Version                 = version,
                                FileVersion             = version,
                                InformationalVersion    = semVersion,
                                Copyright               = copyright,
                                CLSCompliant            = true
                            };
var nuGetPackSettings   = new NuGetPackSettings {
                                Id                      = assemblyInfo.Product,
                                Version                 = assemblyInfo.InformationalVersion,
                                Title                   = assemblyInfo.Title,
                                Authors                 = new[] {assemblyInfo.Company},
                                Owners                  = new[] {assemblyInfo.Company},
                                Description             = assemblyInfo.Description,
                                Summary                 = description,
                                ProjectUrl              = projectUrl,
                                LicenseUrl              = licenseUrl,
                                Copyright               = assemblyInfo.Copyright,
                                ReleaseNotes            = releaseNotes,
                                Tags                    = tags,
                                RequireLicenseAcceptance= false,
                                Symbols                 = false,
                                NoPackageAnalysis       = true,
                                Files                   =  new [] {
                                    new NuSpecContent {Source = "ReSharperReports.exe", Target = "tools"},
                                    new NuSpecContent {Source = "ReSharperReports.pdb", Target = "tools"},
                                    new NuSpecContent {Source = "ReSharperReports.xml", Target = "tools"}
                                },
                                BasePath                = binDir,
                                OutputDirectory         = buildArtifacts
                            };

#l .\Tools\gep13.DefaultBuild\Content\build.cake