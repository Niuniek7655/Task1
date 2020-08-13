using System;

namespace Task1
{
    public class VersionNumberBuilder
    {
        private VersionNumber _version;
        private int setLevelVersion;
        #region ctor
        public VersionNumberBuilder(string versionNumber)
        {
            _version = new VersionNumber(versionNumber);
            setLevelVersion = 0;
        }
        #endregion ctor

        #region public
        public VersionNumberBuilder SetFirstLevelVersion()
        {
            CheckLevelSet(0);
            _version.FirstLevelVersion = SetMainVersionNumber(_version.LevelVersion);
            _version.LevelVersion = RemoveUsePartOfNumber(_version.LevelVersion);
            setLevelVersion++;
            return this;
        }

        public VersionNumberBuilder SetSecondLevelVersion()
        {
            CheckLevelSet(1);
            _version.SecondLevelVersion = SetMainVersionNumber(_version.LevelVersion);
            _version.LevelVersion = RemoveUsePartOfNumber(_version.LevelVersion);
            setLevelVersion++;
            return this;
        }

        public VersionNumberBuilder SetThirdLevelVersion()
        {
            CheckLevelSet(2);
            _version.ThirdLevelVersion = SetMainVersionNumber(_version.LevelVersion);
            _version.LevelVersion = RemoveUsePartOfNumber(_version.LevelVersion);
            setLevelVersion++;
            return this;
        }

        public VersionNumberBuilder SetFourthLevelVersion()
        {
            CheckLevelSet(3);
            _version.FourthLevelVersion = SetVersionNumber(_version.LevelVersion);
            setLevelVersion++;
            return this;
        }

        public VersionNumber Build()
        {
            CheckLevelSet(4);
            return _version;
        }
        #endregion public

        #region private
        private void CheckLevelSet(int level)
        {
            if (setLevelVersion != level)
            {
                throw new BuildException("Kolejność nadawania numeracji jest niepoprawana");
            }
        }

        private int SetMainVersionNumber(string text)
        {
            int? version = SetVersionNumber(text);
            if (version.HasValue)
            {
                return version.Value;
            }
            throw new BuildException("Brak podstawowego numeru wersji");
        }

        private int? SetVersionNumber(string text)
        {
            int charLocation = GetDotLocation(text);
            if (charLocation > 0)
            {
                string version = text.Substring(0, charLocation);
                bool isParseCorrect = int.TryParse(version, out int result);
                if (isParseCorrect)
                {
                    return result;
                }
                return null;
            }
            else
            {
                bool isParseCorrect = int.TryParse(text, out int result);
                if (isParseCorrect)
                {
                    return result;
                }
                return null;
            }
        }

        private int GetDotLocation(string text)
        {
            string stopAt = ".";
            int charLocation = 0;
            if (!string.IsNullOrWhiteSpace(text))
            {
                charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);
            }
            return charLocation;
        }

        private string RemoveUsePartOfNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new BuildException("Brak numeru do edycji");
            }
            int charLocation = GetDotLocation(text);
            if (charLocation == -1)
            {
                text = string.Empty;
            }
            else
            {
                text = text.Substring(charLocation + 1);
            }
            return text;
        }
        #endregion private
    }
}
