#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "ReSharperReports",
                            repositoryOwner: "gep13",
                            repositoryName: "ReSharperReports",
                            appVeyorAccountName: "garyewanpark");

BuildParameters.PrintParamters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.Run();
