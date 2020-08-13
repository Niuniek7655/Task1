using System.Text;

namespace Task1
{
    public class PackageData
    {
        #region ctor
        public PackageData(string nugpackInfo)
        {
            FullName = nugpackInfo;
        }
        #endregion ctor

        #region prop
        public string FullName { get; private set; }
        public string PackageName { get; set; }
        public VersionNumber VersionNumber { get; set; }
        public string VersionSuffix { get; set; }
        public string DisplayFullName 
        { 
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(PackageName);
                builder.Append(".");
                builder.Append(VersionNumber.DisplayVersion);
                if(VersionSuffix != null)
                {
                    builder.Append(VersionSuffix);
                }
                string value = builder.ToString();
                return value;
            }
        }
        #endregion prop
    }
}
