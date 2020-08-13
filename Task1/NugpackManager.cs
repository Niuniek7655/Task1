using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1
{
    public class NugpackManager
    {
        private NugpackNameValidator _validator;
        #region ctor
        public NugpackManager(NugpackNameValidator validator)
        {
            _validator = validator;
        }
        #endregion ctor

        #region public
        public IEnumerable<PackageData> CreatNugpackInfo(IEnumerable<string> nugpackInfo)
        {
            List<PackageData> allNugpackInfo = new List<PackageData>();
            foreach (var element in nugpackInfo)
            {
                PackageDataBuilder builder = new PackageDataBuilder(_validator, element);
                try
                {
                    PackageData packageData = builder
                                                    .SetPackageName()
                                                    .SetVersionNumber()
                                                    .SetVersionSuffix()
                                                    .Build();
                    allNugpackInfo.Add(packageData);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (BuildException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return allNugpackInfo;
        }

        public IEnumerable<PackageData> FindHighestNugPackVersion(IEnumerable<PackageData> nugpackInfo)
        {
            List<PackageData> allPackagesLastesVersions = new List<PackageData>();
            var packageGroup = nugpackInfo
                                        .GroupBy(x => x.PackageName)
                                        .ToDictionary(y => y.Key, y => y.ToList());
            foreach (var element in packageGroup)
            {
                PackageData lastVersion = GetLastestVersion(element.Value);
                allPackagesLastesVersions.Add(lastVersion);
            }
            return allPackagesLastesVersions;
        }
        #endregion public

        #region private
        private PackageData GetLastestVersion(IEnumerable<PackageData> value)
        {
            PackageData lastesVersion = value
                                            .CheckFirstLevel()
                                            .CheckSecondLevel()
                                            .CheckThirdLevel()
                                            .CheckFourthLevel()
                                            .CheckVersionSuffixLevel()
                                            .GetLastesVersion();
            return lastesVersion;
        }
        #endregion private
    }
}
