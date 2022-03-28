using System;
using System.Collections.Generic;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpBlockBuilder
    {
        private readonly CSharpBlock block;

        public CSharpBlockBuilder(string statement)
        {
            block = new CSharpBlock(statement);
        }

        public CSharpBlockBuilder AddStatement(string statement)
        {
            block.Statements.Add(new CSharpStatement(statement));
            return this;
        }

        public CSharpBlockBuilder AddBlock(string statement, Action<CSharpBlockBuilder> blockBuilder)
        {
            var innerBlock = new CSharpBlockBuilder(statement);
            blockBuilder(innerBlock);
            block.Statements.Add(innerBlock.Build());
            return this;
        }

        public CSharpBlock Build()
        {
            return block;
        }
    }
}
