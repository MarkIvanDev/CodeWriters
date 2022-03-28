using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public class CSharpUsing
    {
        private string _namespace;
        private string _alias;

        public string Namespace { get => _namespace; set => _namespace = value?.Trim(); }

        public string Alias { get => _alias; set => _alias = value?.Trim(); }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Alias))
            {
                return $"using {Alias} = {Namespace};";
            }
            else
            {
                return $"using {Namespace};";
            }
        }
    }
}
