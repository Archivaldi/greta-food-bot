using CommandLine;

namespace GretaFoodCore.Api
{
    public class CommandLineArguments
    {
        [Option("mysql-connection-string", Required = true, HelpText = "Mysql connection string")]
        public string MySqlConnectionString { get; set; }

        [Option("tomtom-api-key", Required = true, HelpText = "Tom tom api key")]
        public string TomTomApiKey { get; set; }
    }
}