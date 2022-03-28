using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpAttribute
    {
        public CSharpAttribute(string attribute)
        {
            Attribute = attribute;
        }

        public string Attribute { get; }

        public override string ToString()
        {
            return $"[{Attribute}]";
        }
    }
}
