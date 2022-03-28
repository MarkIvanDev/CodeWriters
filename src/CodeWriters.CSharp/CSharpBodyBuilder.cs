using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpBodyBuilder
    {
        private readonly CSharpBody body;

        public CSharpBodyBuilder()
        {
            body = new CSharpBody();
        }

        public CSharpBodyBuilder AddStatement(string statement)
        {
            body.Statements.Add(new CSharpStatement(statement));
            return this;
        }

        public CSharpBodyBuilder AddBlock(string statement, Action<CSharpBlockBuilder> blockBuilder)
        {
            var innerBlock = new CSharpBlockBuilder(statement);
            blockBuilder(innerBlock);
            body.Statements.Add(innerBlock.Build());
            return this;
        }

        public CSharpBody Build()
        {
            return body;
        }

    }
}
