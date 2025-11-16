using System;

namespace Serjbal.App.MVVM
{
    public class DataAttribute : Attribute
    {
        public string Value { get; private set; }

        public DataAttribute(string value)
        {
            Value = value;
        }
    }
}
