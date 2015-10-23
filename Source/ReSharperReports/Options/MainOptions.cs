using CommandLine;
using CommandLine.Text;

namespace ReSharperReports.Options
{
    public class MainOptions
    {
        [Option('i', "inputFilePath", HelpText = "The path to the input report XML to be transformed.", Required = true)]
        public string InputFilePath { get; set; }

        [Option('o', "outputFilePath", HelpText = "The path to the output report in HTML format.", Required = true)]
        public string OutputFilePath { get; set; }

        [Option('x', "xslFilePath", HelpText = "The path to the input XSL file to be used to perform transformation.", Required = false)]
        public string XslFilePath { get; set; }

        [Option('l', "logFilePath", HelpText = "Path to where log file should be created. Defaults to logging to console.", Required = false)]
        public string LogFilePath { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("ReSharperReports", "0.1.0"),
                Copyright = new CopyrightInfo("gep13", 2015),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: ReSharperReports -i report.xml -o report.html");
            help.AddOptions(this);
            return help;
        }
    }
}