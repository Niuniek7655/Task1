namespace Task1
{
    public class PackageDataBuilder
    {
        private NugpackNameValidator _validator;
        private PackageData _data;
        private bool IsSetPackageName = false;
        private bool IsSetVersionNumber = false;
        private bool IsSetVersionSuffix = false;
        #region ctor
        public PackageDataBuilder(NugpackNameValidator validator, string nugpackInfo)
        {
            _validator = validator;
            _data = new PackageData(nugpackInfo);
        }
        #endregion ctor

        #region public
        public PackageDataBuilder SetPackageName()
        {
            string nugpackInfo = _data.FullName;
            if (string.IsNullOrWhiteSpace(nugpackInfo))
            {
                throw new BuildException("Brak informacji na temat nazwy paczki NuGet");
            }
            _validator.CheckIsNupkg(nugpackInfo);
            int separatorPostion = nugpackInfo.IndexOf('.');
            string nugPackName = nugpackInfo.Substring(0, separatorPostion);
            _validator.CheckPackageName(nugPackName);
            _data.PackageName = nugPackName;
            IsSetPackageName = true;
            return this;
        }

        public PackageDataBuilder SetVersionNumber()
        {
            if (_data.PackageName == null)
            {
                throw new BuildException("Nie ustawiono nazwy paczki NuGet");
            }
            string versionNumber = GetVersionNumberString();
            _validator.CheckVersionNumberString(versionNumber);
            VersionNumberBuilder builder = new VersionNumberBuilder(versionNumber);
            _data.VersionNumber = builder
                                    .SetFirstLevelVersion()
                                    .SetSecondLevelVersion()
                                    .SetThirdLevelVersion()
                                    .SetFourthLevelVersion()
                                    .Build();
            IsSetVersionNumber = true;
            return this;
        }

        public PackageDataBuilder SetVersionSuffix()
        {
            string nugpackInfo = _data.FullName;
            int suffixSeparatoPostion = nugpackInfo.IndexOf("-");
            if (suffixSeparatoPostion != -1)
            {
                string versionSuffix = nugpackInfo.Substring(suffixSeparatoPostion);
                if (versionSuffix.Length > 0)
                {
                    versionSuffix = versionSuffix.Replace(".nupkg", "");
                    _validator.CheckSuffix(versionSuffix);
                    _data.VersionSuffix = versionSuffix;
                }
            }
            IsSetVersionSuffix = true;
            return this;
        }

        public PackageData Build()
        {
            if (IsSetPackageName &&
               IsSetVersionNumber &&
               IsSetVersionSuffix)
            {
                return _data;
            }
            else
            {
                throw new BuildException("Przynajmniej jedna z podstawowych składowych obiektu nie została zdefinowana");
            }
        }
        #endregion public

        #region private
        private string GetVersionNumberString()
        {
            string nugpackInfo = _data.FullName;
            nugpackInfo = nugpackInfo.Replace(_data.PackageName, "");
            nugpackInfo = nugpackInfo.Replace("nupkg", "");
            if (nugpackInfo.Length > 0)
            {
                nugpackInfo = nugpackInfo.Remove(nugpackInfo.Length - 1);
            }

            int suffixSeparatoPostion = nugpackInfo.IndexOf("-");
            string versionNumber = null;
            if (suffixSeparatoPostion == -1)
            {
                versionNumber = nugpackInfo;
            }
            else
            {
                string versionSuffixu = nugpackInfo.Substring(suffixSeparatoPostion);
                versionNumber = nugpackInfo.Replace(versionSuffixu, "");
            }
            return versionNumber;
        }
        #endregion private
    }
}
