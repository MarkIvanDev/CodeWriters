using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpFieldBuilder
    {
        private readonly CSharpField field;

        public CSharpFieldBuilder(string type, string name)
        {
            field = new CSharpField(type, name);
        }

        public CSharpFieldBuilder WithAccessLevel(AccessLevel accessLevel)
        {
            field.AccessLevel = accessLevel;
            return this;
        }

        public CSharpFieldBuilder IsStatic()
        {
            field.IsStatic = true;
            return this;
        }

        public CSharpFieldBuilder IsReadOnly()
        {
            field.IsReadOnly = true;
            return this;
        }

        public CSharpFieldBuilder AddAttribute(string attribute)
        {
            field.Attributes.Add(attribute);
            return this;
        }

        public CSharpField Build()
        {
            return field;
        }
    }
}
