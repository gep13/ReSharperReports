using CommandLine;

namespace ReSharperReports.Options
{
    /// <summary>
    /// The options for the Transform Verb
    /// </summary>
    [Verb("transform", HelpText = "Transforms an input XML report file.")]
    public class TransformSubOptions
    {
        /// <summary>
        /// Gets or sets the input file path
        /// </summary>
        [Option('i', "inputFilePath", HelpText = "The path to the input report XML to be transformed.", Required = true)]
        public string InputFilePath { get; set; }

        /// <summary>
        /// Gets or sets the output file path
        /// </summary>
        [Option('o', "outputFilePath", HelpText = "The path to the output report in HTML format.", Required = true)]
        public string OutputFilePath { get; set; }

        /// <summary>
        /// Gets or sets the XSL Transformation file path
        /// </summary>
        /// /// <remarks>This is an optional input</remarks>
        [Option('x', "xslFilePath", HelpText = "The path to the input XSL file to be used to perform transformation.", Required = false)]
        public string XslFilePath { get; set; }

        /// <summary>
        /// Gets or sets the log file path
        /// </summary>
        /// <remarks>This is an optional input</remarks>
        [Option('l', "logFilePath", HelpText = "Path to where log file should be created. Defaults to logging to console.", Required = false)]
        public string LogFilePath { get; set; }
    }
}