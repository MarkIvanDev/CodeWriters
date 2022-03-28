using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpConstructorBuilder
    {
        private readonly CSharpConstructor constructor;
        private readonly List<CSharpParameterBuilder> parameters;

        public CSharpConstructorBuilder(string name)
        {
            constructor = new CSharpConstructor(name);
            parameters = new List<CSharpParameterBuilder>();
        }

        public CSharpConstructorBuilder WithAccessLevel(AccessLevel accessLevel)
        {
            constructor.AccessLevel = accessLevel;
            return this;
        }

        public CSharpConstructorBuilder IsStatic()
        {
            constructor.IsStatic = true;
            return this;
        }

        public CSharpConstructorBuilder AddAttribute(string attribute)
        {
            constructor.Attributes.Add(new CSharpAttribute(attribute));
            return this;
        }

        public CSharpParameterBuilder AddParameter<T>(string name) => AddParameter(typeof(T).FullName, name);

        public CSharpParameterBuilder AddParameter(string type, string name)
        {
            var parameter = new CSharpParameterBuilder(type, name);
            parameters.Add(parameter);
            return parameter;
        }

        public CSharpConstructorBuilder WithBody(Action<CSharpBodyBuilder> bodyBuilder)
        {
            var body = new CSharpBodyBuilder();
            bodyBuilder(body);
            constructor.Body = body.Build();
            return this;
        }

        public CSharpConstructor Build()
        {
            foreach (var item in parameters)
            {
                constructor.Parameters.Add(item.Build());
            }

            return constructor;
        }

    }
}
