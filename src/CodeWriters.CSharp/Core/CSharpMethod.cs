using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpMethod
    {
        public CSharpMethod(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public AccessLevel AccessLevel { get; set; }

        public bool IsStatic { get; set; }

        public bool IsPartial { get; set; }

        public bool IsAsync { get; set; }

        public string ReturnType { get; set; }

        public List<CSharpAttribute> Attributes { get; } = new List<CSharpAttribute>();

        public List<CSharpParameter> Parameters { get; } = new List<CSharpParameter>();

        public CSharpBody Body { get; set; }

        public string GetHeader()
        {
            return $"{AccessLevel.GetDescription()}{(IsStatic ? "static " : "")}{(IsPartial ? "partial " : "")}{(IsAsync ? "async " : "")}{ReturnType} {Name}({string.Join(", ", Parameters.Select(p => p.ToString()))})";
        }
    }
}
