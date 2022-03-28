using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpField
    {
        public CSharpField(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public List<string> Attributes { get; } = new List<string>();

        public string Type { get; }

        public string Name { get; }

        public AccessLevel AccessLevel { get; set; }

        public bool IsStatic { get; set; }

        public bool IsReadOnly { get; set; }

        public override string ToString()
        {
            return $"{AccessLevel.GetDescription()}{(IsStatic ? "static " : "")}{(IsReadOnly ? "readonly " : "")}{Type} {Name};";
        }
    }
}
