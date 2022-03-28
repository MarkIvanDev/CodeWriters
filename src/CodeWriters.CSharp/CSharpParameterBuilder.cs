using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpParameterBuilder
    {
        private readonly CSharpParameter parameter;

        public CSharpParameterBuilder(string type, string name)
        {
            parameter = new CSharpParameter(type, name);
        }

        public CSharpParameterBuilder AddAttribute(string attribute)
        {
            parameter.Attributes.Add(new CSharpAttribute(attribute));
            return this;
        }

        public CSharpParameterBuilder WithModifier(CSharpParameterModifier modifier)
        {
            parameter.Modifier = modifier;
            return this;
        }

        public CSharpParameterBuilder WithDefaultValue(string defaultValue)
        {
            parameter.DefaultValue = defaultValue;
            return this;
        }

        public CSharpParameter Build()
        {
            return parameter;
        }
    }
}
