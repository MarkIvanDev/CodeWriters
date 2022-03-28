using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpPropertyBuilder
    {
        private readonly CSharpProperty property;
        private CSharpFieldBuilder backingField;

        public CSharpPropertyBuilder(string type, string name)
        {
            property = new CSharpProperty(type, name);
            backingField = null;
        }

        public CSharpPropertyBuilder WithAccessLevel(AccessLevel accessLevel)
        {
            property.AccessLevel = accessLevel;
            return this;
        }

        public CSharpPropertyBuilder IsStatic()
        {
            property.IsStatic = true;
            return this;
        }

        public CSharpPropertyBuilder WithBackingField(string name)
        {
            backingField = new CSharpFieldBuilder(property.Type, name);
            return this;
        }

        public CSharpPropertyBuilder AddAttribute(string attribute)
        {
            property.Attributes.Add(new CSharpAttribute(attribute));
            return this;
        }

        public CSharpPropertyBuilder WithGetter(AccessLevel accessLevel, Action<CSharpBodyBuilder> body)
        {
            var bodyBuilder = new CSharpBodyBuilder();
            body(bodyBuilder);
            var getter = new CSharpPropertyGetter()
            {
                AccessLevel = accessLevel,
                Body = bodyBuilder.Build()
            };
            property.Getter = getter;
            return this;
        }

        public CSharpPropertyBuilder WithSetter(AccessLevel accessLevel, Action<CSharpBodyBuilder> body)
        {
            var bodyBuilder = new CSharpBodyBuilder();
            body(bodyBuilder);
            var setter = new CSharpPropertySetter()
            {
                AccessLevel = accessLevel,
                Body = bodyBuilder.Build()
            };
            property.Setter = setter;
            return this;
        }

        public CSharpProperty Build()
        {
            if (backingField != null)
            {
                property.BackingField = backingField.Build(); 
            }

            return property;
        }
    }
}
