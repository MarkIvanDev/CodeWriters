using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpConstructor
    {
        public CSharpConstructor(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public AccessLevel AccessLevel { get; set; }

        public bool IsStatic { get; set; }

        public List<CSharpAttribute> Attributes { get; } = new List<CSharpAttribute>();

        public List<CSharpParameter> Parameters { get; } = new List<CSharpParameter>();

        public CSharpBody Body { get; set; }
    }
}
