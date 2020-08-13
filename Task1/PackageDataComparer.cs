using System.Collections.Generic;
using System.Linq;

namespace Task1
{
    public static class PackageDataComparer
    {
        public static IEnumerable<PackageData> CheckFirstLevel(this IEnumerable<PackageData> value)
        {
            int lastesVersion = value
                                    .Select(x => x.VersionNumber.FirstLevelVersion)
                                    .Max();
            var allObjectOnThisVersion = value.Where(x => x.VersionNumber.FirstLevelVersion == lastesVersion);
            return allObjectOnThisVersion;
        }

        public static IEnumerable<PackageData> CheckSecondLevel(this IEnumerable<PackageData> value)
        {
            int lastesVersion = value
                                    .Select(x => x.VersionNumber.SecondLevelVersion)
                                    .Max();
            var allObjectOnThisVersion = value.Where(x => x.VersionNumber.SecondLevelVersion == lastesVersion);
            return allObjectOnThisVersion;
        }

        public static IEnumerable<PackageData> CheckThirdLevel(this IEnumerable<PackageData> value)
        {
            int lastesVersion = value
                                    .Select(x => x.VersionNumber.ThirdLevelVersion)
                                    .Max();
            var allObjectOnThisVersion = value.Where(x => x.VersionNumber.ThirdLevelVersion == lastesVersion);
            return allObjectOnThisVersion;
        }

        public static IEnumerable<PackageData> CheckFourthLevel(this IEnumerable<PackageData> value)
        {
            int? lastesVersion = value
                                    .Select(x => x.VersionNumber.FourthLevelVersion)
                                    .Max();
            var allObjectOnThisVersion = value.Where(x => x.VersionNumber.FourthLevelVersion == lastesVersion);
            return allObjectOnThisVersion;
        }

        public static IEnumerable<PackageData> CheckVersionSuffixLevel(this IEnumerable<PackageData> value)
        {
            var allPackagesWithoutSuffix = value.Where(x => x.VersionSuffix == null);
            if (allPackagesWithoutSuffix.Count() == 1)
            {
                return allPackagesWithoutSuffix;
            }
            var allObjectOnThisVersion = value.OrderByDescending(x => x.VersionSuffix);
            return allObjectOnThisVersion;
        }

        public static PackageData GetLastesVersion(this IEnumerable<PackageData> value)
        {
            PackageData lastesVersion = value.First();
            return lastesVersion;
        }
    }
}
