using System;

namespace Task1
{
    public class BuildException : Exception
    {
        public BuildException(string message) : base(message)
        {

        }
    }
}
