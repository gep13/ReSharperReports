///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// PROJECT SPECIFIC VARIABLES
///////////////////////////////////////////////////////////////////////////////
var solutionFilePath          = "./Source/ReSharperReports.sln";
var solutionDirectoryPath     = "./Source/ReSharperReports";
var sourceDirectoryPath       = "./Source";
var binDirectoryPath          = "./Source/ReSharperReports/bin/" + configuration;
var buildDirectoryPath        = "./.build";
var title                     = "ReSharperReports";
var resharperSettingsFileName = "/ReSharperReports.sln.DotSettings";
var description               = "Command line tool to allow generation of human readable ReSharper Reports";
var product                   = "ReSharperReports";
var company                   = "gep13";
var copyright                 = string.Format("Copyright © gep13 and Contributors {0} - Present", DateTime.Now.Year);
var projectUrl                = new Uri("https://github.com/gep13/ReSharperReports/");
var licenseUrl                = new Uri("https://github.com/gep13/ReSharperReports/blob/master/LICENSE");
var releaseNotes              = new List<string>() { "https://github.com/gep13/ReSharperReports/releases" };
var tags                      = new [] {"ReSharper", "DupFinder", "InspectCode", "Reports"};
var nugetFiles                = new [] {
                                          new NuSpecContent {Source = "ReSharperReports.exe", Target = "tools"},
                                          new NuSpecContent {Source = "ReSharperReports.pdb", Target = "tools"},
                                          new NuSpecContent {Source = "ReSharperReports.xml", Target = "tools"}
                                      };
                                
var semVersion                = "0.1.0";

#l .\Tools\gep13.DefaultBuild\Content\build.cake