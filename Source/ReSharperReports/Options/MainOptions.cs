namespace ReSharperReports.Options
{
    /// <summary>
    /// The defined set of arguments which can be passed in from the command line
    /// </summary>
    public class MainOptions
    {
        /// <summary>
        /// The transform function
        /// </summary>
        public TransformSubOptions TransformVerb { get; set; }
    }
}