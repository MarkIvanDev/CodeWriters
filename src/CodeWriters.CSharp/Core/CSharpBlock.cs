using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpBlock : CSharpStatement
    {
        public CSharpBlock(string statement) : base(statement)
        {
        }

        public List<CSharpStatement> Statements { get; } = new List<CSharpStatement>();
    }
}
