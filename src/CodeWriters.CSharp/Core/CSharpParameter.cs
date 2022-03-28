using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpParameter
    {
        public CSharpParameter(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }

        public string Name { get; }

        public CSharpParameterModifier Modifier { get; set; }

        public string DefaultValue { get; set; }

        public List<CSharpAttribute> Attributes { get; } = new List<CSharpAttribute>();

        public override string ToString()
        {
            return $"{(Attributes.Count > 0 ? $"{string.Join("", Attributes.Select(a => a.ToString()))} " : "")}{Modifier.GetDescription()}{Type} {Name}{(DefaultValue != null ? $" = {DefaultValue}" : "")}";
        }
    }

    public enum CSharpParameterModifier
    {
        [Description("")]
        None = 0,

        [Description("params ")]
        Params = 1,

        [Description("in ")]
        In = 2,

        [Description("ref ")]
        Ref = 3,

        [Description("out ")]
        Out = 4
    }

    public static class CSharpParameterModifierExtensions
    {
        public static string GetDescription(this CSharpParameterModifier accessLevel)
        {
            var name = accessLevel.ToString();
            return typeof(CSharpParameterModifier)
                .GetField(name)?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description ?? name;
        }
    }
}
