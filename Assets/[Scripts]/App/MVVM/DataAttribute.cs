using System;

namespace Serjbal.App.MVVM
{
    public class DataAttribute : Attribute
    {
        public string Key { get; private set; }

        public DataAttribute(string value)
        {
            Key = value;
        }
    }
}
