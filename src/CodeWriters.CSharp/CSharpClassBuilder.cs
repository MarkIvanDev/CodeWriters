using System;
using System.Collections.Generic;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpClassBuilder
    {
        private readonly CSharpClass csharpClass;
        private readonly List<CSharpFieldBuilder> fields;
        private readonly List<CSharpConstructorBuilder> constructors;
        private readonly List<CSharpPropertyBuilder> properties;
        private readonly List<CSharpMethodBuilder> methods;

        public CSharpClassBuilder(string name)
        {
            csharpClass = new CSharpClass(name);
            fields = new List<CSharpFieldBuilder>();
            constructors = new List<CSharpConstructorBuilder>();
            properties = new List<CSharpPropertyBuilder>();
            methods = new List<CSharpMethodBuilder>();
        }

        public CSharpClassBuilder AddUsing(string ns, string alias = null)
        {
            csharpClass.Usings.Add(new CSharpUsing
            {
                Namespace = ns,
                Alias = alias
            });
            return this;
        }

        public CSharpClassBuilder WithNamespace(string ns)
        {
            csharpClass.Namespace = ns;
            return this;
        }

        public CSharpClassBuilder WithAccessLevel(AccessLevel accessLevel)
        {
            csharpClass.AccessLevel = accessLevel;
            return this;
        }

        public CSharpClassBuilder IsStatic()
        {
            csharpClass.IsStatic = true;
            return this;
        }

        public CSharpClassBuilder IsSealed()
        {
            csharpClass.IsSealed = true;
            return this;
        }

        public CSharpClassBuilder IsAbstract()
        {
            csharpClass.IsAbstract = true;
            return this;
        }

        public CSharpClassBuilder IsPartial()
        {
            csharpClass.IsPartial = true;
            return this;
        }

        public CSharpClassBuilder AddAttribute(string attribute)
        {
            csharpClass.Attributes.Add(new CSharpAttribute(attribute));
            return this;
        }

        public CSharpFieldBuilder AddField<T>(string name) => AddField(typeof(T).FullName, name);

        public CSharpFieldBuilder AddField(string type, string name)
        {
            var field = new CSharpFieldBuilder(type, name);
            fields.Add(field);
            return field;
        }

        public CSharpConstructorBuilder AddConstructor()
        {
            var constructor = new CSharpConstructorBuilder(csharpClass.Name);
            constructors.Add(constructor);
            return constructor;
        }

        public CSharpPropertyBuilder AddProperty<T>(string name) => AddProperty(typeof(T).FullName, name);

        public CSharpPropertyBuilder AddProperty(string type, string name)
        {
            var property = new CSharpPropertyBuilder(type, name);
            properties.Add(property);
            return property;
        }

        public CSharpMethodBuilder AddMethod(string name)
        {
            var method = new CSharpMethodBuilder(name);
            methods.Add(method);
            return method;
        }

        public CSharpClass Build()
        {
            foreach (var item in fields)
            {
                csharpClass.Fields.Add(item.Build());
            }

            foreach (var item in constructors)
            {
                csharpClass.Constructors.Add(item.Build());
            }

            foreach (var item in properties)
            {
                csharpClass.Properties.Add(item.Build());
            }

            foreach (var item in methods)
            {
                csharpClass.Methods.Add(item.Build());
            }

            return csharpClass;
        }

    }
}
