using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpClass
    {
        private string _namespace;

        public CSharpClass(string name)
        {
            Name = name?.Trim();
        }

        public List<CSharpUsing> Usings { get; } = new List<CSharpUsing>();

        public string Namespace { get => _namespace; set => _namespace = value?.Trim(); }

        public string Name { get; }

        public AccessLevel AccessLevel { get; set; }

        public bool IsStatic { get; set; }

        public bool IsSealed { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsPartial { get; set; }

        public List<CSharpAttribute> Attributes { get; } = new List<CSharpAttribute>();

        public List<CSharpField> Fields { get; } = new List<CSharpField>();

        public List<CSharpConstructor> Constructors { get; } = new List<CSharpConstructor>();

        public List<CSharpProperty> Properties { get; } = new List<CSharpProperty>();

        public List<CSharpMethod> Methods { get; } = new List<CSharpMethod>();

        public string NamespaceHeader => $"namespace {Namespace}";

        public string ClassHeader => $"{AccessLevel.GetDescription()}{(IsStatic ? "static " : "")}class {Name}";
    }
}
