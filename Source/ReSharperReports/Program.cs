using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using ReSharperReports.Options;

namespace ReSharperReports
{
    /// <summary>
    /// The main entry point
    /// </summary>
    public static class Program
    {
        private static StringBuilder log = new StringBuilder();

        /// <summary>
        /// Entry point for the console application
        /// </summary>
        /// <param name="args">The passed in argumnets from command line</param>
        private static void Main(string[] args)
        {
            var options = new MainOptions();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                TransformReport(options);
            }
        }

        private static void TransformReport(MainOptions options)
        {
            ConfigureLogging(options.LogFilePath);

            var inputFilePath = options.InputFilePath;

            if (!File.Exists(inputFilePath))
            {
                Logger.WriteError("Unable to locate Input File");
                return;
            }

            var outputFilePath = options.OutputFilePath;

            var rootNodeName = string.Empty;

            // find out what type of report it is
            using (var xmlReader = XmlReader.Create(inputFilePath))
            {
                if (xmlReader.MoveToContent() == XmlNodeType.Element)
                {
                    rootNodeName = xmlReader.Name;
                }
            }

            if (string.IsNullOrWhiteSpace(rootNodeName))
            {
                Logger.WriteError("Unable to determine root node of Input File");
                return;
            }

            var xslFileName = string.Empty;
            switch (rootNodeName)
            {
                case "DuplicatesReport":
                    xslFileName = "ReSharperReports.dupfinder.xsl";
                    break;
                case "Report":
                    xslFileName = "ReSharperReports.inspectcode.xsl";
                    break;
                default:
                    Logger.WriteError("Unable to parse input XML file.");
                    break;
            }

            var xslFilePath = options.XslFilePath;
            if (string.IsNullOrWhiteSpace(xslFilePath))
            {
                using (var xslStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(xslFileName))
                {
                    using (var reader = XmlReader.Create(xslStream))
                    {
                        var xslt = new XslCompiledTransform();
                        xslt.Load(reader);
                        xslt.Transform(inputFilePath, outputFilePath);

                        Logger.WriteInfo("XSL Transformation complete");
                    }
                }
            }
            else
            {
                if (!File.Exists(xslFilePath))
                {
                    Logger.WriteError("Unable to find specified XSL Transform File.");
                    return;
                }

                var xslt = new XslCompiledTransform();
                xslt.Load(xslFilePath);
                xslt.Transform(inputFilePath, outputFilePath);

                Logger.WriteInfo("XSL Transformation complete");
            }
        }

        private static void ConfigureLogging(string logFilePath)
        {
            var writeActions = new List<Action<string>>
            {
                s => log.AppendLine(s)
            };

            if (!string.IsNullOrEmpty(logFilePath))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                    if (File.Exists(logFilePath))
                    {
                        using (File.CreateText(logFilePath))
                        {
                        }
                    }

                    writeActions.Add(x => WriteLogEntry(logFilePath, x));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to configure logging: " + ex.Message);
                }
            }
            else
            {
                // if nothing else is specified, write to console
                writeActions.Add(Console.WriteLine);
            }

            Logger.WriteInfo = s => writeActions.ForEach(a => a(s));
            Logger.WriteWarning = s => writeActions.ForEach(a => a(s));
            Logger.WriteError = s => writeActions.ForEach(a => a(s));
        }

        private static void WriteLogEntry(string logFilePath, string s)
        {
            var contents = string.Format(CultureInfo.InvariantCulture, "{0}\t\t{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), s);
            File.AppendAllText(logFilePath, contents);
        }
    }
}