using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpStatement
    {
        public CSharpStatement(string statement)
        {
            Statement = statement;
        }

        public string Statement { get; }
    }
}
