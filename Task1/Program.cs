using System;
using System.IO;
using System.Linq;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            NugpackNameValidator validator = new NugpackNameValidator();
            NugpackManager manager = new NugpackManager(validator);
            if (args.Length < 1)
            {
                Console.WriteLine("Provide path.");
                Console.ReadLine();
                return;
            }

            var packagesDirectory = new DirectoryInfo(args[0]);

            if (!packagesDirectory.Exists)
            {
                Console.WriteLine("Provided path does not exist.");
                Console.ReadLine();
                return;
            }

            var nugpackName = packagesDirectory.EnumerateFiles("*.nupkg").Select(x => x.Name);
            var nugPackInfo = manager.CreatNugpackInfo(nugpackName);
            var allPackagesLastesVersions = manager.FindHighestNugPackVersion(nugPackInfo);

            foreach (var nupkgFile in allPackagesLastesVersions)
            {
                Console.WriteLine(nupkgFile.DisplayFullName);
            }

            Console.ReadLine();
        }
    }
}
