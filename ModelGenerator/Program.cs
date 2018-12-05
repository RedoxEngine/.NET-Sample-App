using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            //expect args to contain the path for the JsonSchemas folder
            if (args.Length == 2 && Path.GetFileName(args[0]) == "JsonSchemas")
            {
                Console.WriteLine($"Processing files in {args[0]}");
                Console.WriteLine($"Output files in {args[1]}");
                ProcessDirectory(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("Incorrect number of arguments. Provide the folder for the JsonSchemas and the output folder.");
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private static void ProcessDirectory(string rootDirectory, string targetDirectory)
        {
            ProcessDirectoryRecursive(rootDirectory, file => ProcessFile(file, rootDirectory, targetDirectory));
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.        
        private static void ProcessDirectoryRecursive(string currentDirectory, Action<string> processFile) { 
            // Process the list of files found in the directory.
            var fileEntries = Directory.GetFiles(currentDirectory);
            foreach (var fileName in fileEntries)
            {
                processFile(fileName);
            }

            // Recurse into subdirectories of this directory.
            var subdirectoryEntries = Directory.GetDirectories(currentDirectory);
            foreach (var subdirectory in subdirectoryEntries)
            {
                ProcessDirectoryRecursive(subdirectory, processFile);
            }
        }

        private static void ProcessFile(string filename, string rootDirectory, string targetDirectory)
        {

            var myTI = new CultureInfo("en-US", false).TextInfo;

            var newFileName = myTI.ToTitleCase(filename.Replace(rootDirectory, targetDirectory).Replace(".json", ".cs"));

            var newFileDirectory = new System.IO.FileInfo(newFileName).Directory;
            newFileDirectory.Create(); // If the directory already exists, this method does nothing.

            if (File.Exists(newFileName))
                File.Delete(newFileName);

            var className = Path.GetFileNameWithoutExtension(newFileName);  //name the class the same as the file
            var nameSpace = string.Join(".", "RedoxApi", "Models", myTI.ToTitleCase(newFileDirectory.Name), myTI.ToTitleCase(className));
            var schema = JsonSchema4.FromFileAsync(filename).Result;
            var settings = new CSharpGeneratorSettings { ClassStyle = CSharpClassStyle.Poco, Namespace = nameSpace};
            var generator = new CSharpGenerator(schema, settings);

            
            var fileContents = generator.GenerateFile(className);
            if (!string.IsNullOrEmpty(fileContents))
            {
                File.WriteAllText(newFileName, fileContents);
                Console.Write(".");
            }
            else
            {
                Console.WriteLine($"Something went wrong with {filename}");
            }
        }
    }
}
