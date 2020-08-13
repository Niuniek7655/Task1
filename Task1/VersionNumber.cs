using System.Text;

namespace Task1
{
    public class VersionNumber
    {
        #region ctor
        public VersionNumber(string versionNumber)
        {
            if (string.IsNullOrWhiteSpace(versionNumber))
            {
                throw new BuildException("Brak informacji na temat numeru wersji paczki NuGet");
            }
            LevelVersion = versionNumber.Substring(1);
        }
        #endregion ctor

        #region prop
        public string LevelVersion { get; set; }
        public int FirstLevelVersion { get; set; }
        public int SecondLevelVersion { get; set; }
        public int ThirdLevelVersion { get; set; }
        public int? FourthLevelVersion { get; set; }
        public string DisplayVersion
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(FirstLevelVersion);
                builder.Append(".");
                builder.Append(SecondLevelVersion);
                builder.Append(".");
                builder.Append(ThirdLevelVersion);
                builder.Append(".");
                if(FourthLevelVersion == null)
                {
                    builder.Append("0");
                }
                else
                {
                    builder.Append(FourthLevelVersion);
                }
                string value = builder.ToString();
                return value;
            }
        }
        #endregion prop
    }
}
