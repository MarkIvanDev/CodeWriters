using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CodeWriters.CSharp.Core
{
    public enum AccessLevel
    {
        [Description("public ")]
        Public = 0,

        [Description("protected internal ")]
        ProtectedInternal = 1,

        [Description("internal ")]
        Internal = 2,

        [Description("protected ")]
        Protected = 3,

        [Description("private protected ")]
        PrivateProtected = 4,

        [Description("private ")]
        Private = 5,

    }

    public static class AccessLevelExtensions
    {
        public static string GetDescription(this AccessLevel accessLevel)
        {
            var name = accessLevel.ToString();
            return typeof(AccessLevel)
                .GetField(name)?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description ?? name;
        }
    }
}
