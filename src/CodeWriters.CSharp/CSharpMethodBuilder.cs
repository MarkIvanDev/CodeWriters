using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpMethodBuilder
    {
        private readonly CSharpMethod method;
        private readonly List<CSharpParameterBuilder> parameters;

        public CSharpMethodBuilder(string name)
        {
            method = new CSharpMethod(name);
            parameters = new List<CSharpParameterBuilder>();
        }

        public CSharpMethodBuilder WithAccessLevel(AccessLevel accessLevel)
        {
            method.AccessLevel = accessLevel;
            return this;
        }

        public CSharpMethodBuilder IsStatic()
        {
            method.IsStatic = true;
            return this;
        }

        public CSharpMethodBuilder IsPartial()
        {
            method.IsPartial = true;
            return this;
        }

        public CSharpMethodBuilder IsAsync()
        {
            method.IsAsync = true;
            return this;
        }

        public CSharpMethodBuilder WithReturnType<T>() => WithReturnType(typeof(T).FullName);

        public CSharpMethodBuilder WithReturnType(string returnType)
        {
            method.ReturnType = returnType;
            return this;
        }

        public CSharpMethodBuilder AddAttribute(string attribute)
        {
            method.Attributes.Add(new CSharpAttribute(attribute));
            return this;
        }

        public CSharpParameterBuilder AddParameter<T>(string name) => AddParameter(typeof(T).FullName, name);

        public CSharpParameterBuilder AddParameter(string type, string name)
        {
            var parameter = new CSharpParameterBuilder(type, name);
            parameters.Add(parameter);
            return parameter;
        }

        public CSharpMethodBuilder WithBody(Action<CSharpBodyBuilder> bodyBuilder)
        {
            var body = new CSharpBodyBuilder();
            bodyBuilder(body);
            method.Body = body.Build();
            return this;
        }

        public CSharpMethod Build()
        {
            foreach (var item in parameters)
            {
                method.Parameters.Add(item.Build());
            }

            return method;
        }
    }
}
