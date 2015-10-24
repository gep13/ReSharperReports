using CommandLine;
using CommandLine.Text;

namespace ReSharperReports.Options
{
    /// <summary>
    /// The defined set of arguments which can be passed in from the command line
    /// </summary>
    public class MainOptions
    {
        /// <summary>
        /// Gets or set the input file path
        /// </summary>
        [Option('i', "inputFilePath", HelpText = "The path to the input report XML to be transformed.", Required = true)]
        public string InputFilePath { get; set; }

        /// <summary>
        /// Gets or set the output file path
        /// </summary>
        [Option('o', "outputFilePath", HelpText = "The path to the output report in HTML format.", Required = true)]
        public string OutputFilePath { get; set; }

        /// <summary>
        /// Gets or set the XSL Transformation file path
        /// </summary>
        /// /// <remarks>This is an optional input</remarks>
        [Option('x', "xslFilePath", HelpText = "The path to the input XSL file to be used to perform transformation.", Required = false)]
        public string XslFilePath { get; set; }

        /// <summary>
        /// Gets or set the log file path
        /// </summary>
        /// <remarks>This is an optional input</remarks>
        [Option('l', "logFilePath", HelpText = "Path to where log file should be created. Defaults to logging to console.", Required = false)]
        public string LogFilePath { get; set; }

        /// <summary>
        /// Helper method to construct the usage information for the command line tool
        /// </summary>
        /// <returns>The complete help information</returns>
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