using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpProperty
    {
        public CSharpProperty(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }

        public string Name { get; }

        public AccessLevel AccessLevel { get; set; }

        public bool IsStatic { get; set; }

        public CSharpField BackingField { get; set; }

        public CSharpPropertyGetter Getter { get; set; }

        public CSharpPropertySetter Setter { get; set; }

        public List<CSharpAttribute> Attributes { get; } = new List<CSharpAttribute>();
    }

    public class CSharpPropertyGetter
    {
        public AccessLevel AccessLevel { get; set; }

        public CSharpBody Body { get; set; }
    }

    public class CSharpPropertySetter
    {
        public AccessLevel AccessLevel { get; set; }

        public CSharpBody Body { get; set; }
    }
}
