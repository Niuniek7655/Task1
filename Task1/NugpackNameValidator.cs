using System.Text.RegularExpressions;

namespace Task1
{
    public class NugpackNameValidator
    {
        public void CheckIsNupkg(string nugpackInfo)
        {
            bool isContain = nugpackInfo.Contains(".nupkg");
            if (!isContain)
            {
                throw new ValidationException($"Wskazany plik nie jest paczką Nuget'ową: {nugpackInfo}");
            }
        }

        public void CheckPackageName(string nugPackName)
        {
            bool isMatch = Regex.IsMatch(nugPackName, @"^[a-zA-Z]+$");
            if (!isMatch)
            {
                throw new ValidationException("Nazwa wskazanej paczki nie zawiera wyłącznie liter");
            }
        }

        public void CheckVersionNumberString(string versionNumber)
        {
            bool isMatch = Regex.IsMatch(versionNumber, @"^[0-9.]+$");
            if (!isMatch)
            {
                throw new ValidationException("Numer wersji paczki zawiera niedozwolone znaki");
            }
        }

        public void CheckSuffix(string versionSuffix)
        {
            bool isContainDash = versionSuffix[0] == '-';
            string suffixName = versionSuffix.Substring(1);
            bool isMatch = Regex.IsMatch(suffixName, @"^[a-zA-Z0-9]+$");
            if(isContainDash && isMatch)
            {
                return;
            }
            throw new ValidationException("Nie poprawny format suffix'a paczki");
        }
    }
}
