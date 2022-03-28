using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeWriters.CSharp.Core;

namespace CodeWriters.CSharp
{
    public class CSharpCodeWriter
    {
        private readonly StringBuilder content;
        private readonly ScopeTracker scopeTracker;
        private readonly object writeLock;
        private int indentLevel;

        public CSharpCodeWriter()
        {
            content = new StringBuilder();
            scopeTracker = new ScopeTracker(this);
            writeLock = new object();
            indentLevel = 0;
        }

        private void Append(string text) => content.Append(text);

        private void AppendLine(string text) => content.Append(new string('\t', indentLevel)).AppendLine(text);

        private void AppendLine() => content.AppendLine();

        private void AddIndent() => indentLevel += 1;

        private void RemoveIndent() => indentLevel -= 1;

        private IDisposable BeginScope(string line)
        {
            AppendLine(line);
            return BeginScope();
        }

        private IDisposable BeginScope()
        {
            AppendLine("{");
            AddIndent();
            return scopeTracker;
        }

        private void EndScope()
        {
            RemoveIndent();
            AppendLine("}");
        }

        public string Write(CSharpClass csharpClass)
        {
            lock (writeLock)
            {
                content.Clear();
                indentLevel = 0;

                foreach (var item in csharpClass.Usings)
                {
                    InnerWrite(item);
                }
                AppendLine();

                var namespaceScope = !string.IsNullOrEmpty(csharpClass.Namespace) ?
                    BeginScope(csharpClass.NamespaceHeader) : null;
                if (namespaceScope != null)
                {
                    AppendLine();
                }

                foreach (var item in csharpClass.Attributes)
                {
                    InnerWrite(item);
                }
                using (var classScope = BeginScope(csharpClass.ClassHeader))
                {
                    foreach (var item in csharpClass.Fields)
                    {
                        InnerWrite(item);
                    }
                    AppendLine();

                    foreach (var item in csharpClass.Constructors)
                    {
                        InnerWrite(item);
                        AppendLine();
                    }
                    AppendLine();

                    foreach (var item in csharpClass.Properties)
                    {
                        InnerWrite(item);
                        AppendLine();
                    }

                    foreach (var item in csharpClass.Methods)
                    {
                        InnerWrite(item);
                        AppendLine();
                    }
                }

                namespaceScope?.Dispose();

                return content.ToString();
            }
        }

        private void InnerWrite(CSharpUsing @using)
        {
            AppendLine(@using.ToString());
        }

        private void InnerWrite(CSharpAttribute attribute)
        {
            AppendLine(attribute.ToString());
        }

        private void InnerWrite(CSharpField field)
        {
            AppendLine(field.ToString());
        }

        private void InnerWrite(CSharpConstructor constructor)
        {
            foreach (var item in constructor.Attributes)
            {
                InnerWrite(item);
            }
            using (var constructorScope = BeginScope(constructor.IsStatic ? $"static {constructor.Name}()" :
                $"{constructor.AccessLevel.GetDescription()}{constructor.Name}({string.Join(", ", constructor.Parameters.Select(p => p.ToString()))})"))
            {
                if (constructor.Body != null)
                {
                    foreach (var s in constructor.Body.Statements)
                    {
                        InnerWrite(s);
                    }
                }
            }
        }

        private void InnerWrite(CSharpStatement statement)
        {
            if (statement is CSharpBlock block)
            {
                using (BeginScope(block.Statement))
                {
                    foreach (var item in block.Statements)
                    {
                        InnerWrite(item);
                    }
                }
            }
            else
            {
                AppendLine(statement.Statement);
            }
        }

        private void InnerWrite(CSharpProperty property)
        {
            if (property.BackingField != null)
            {
                InnerWrite(property.BackingField);
                AppendLine();
            }

            foreach (var item in property.Attributes)
            {
                InnerWrite(item);
            }
            if (property.Getter is null && property.Setter is null)
            {
                var header = $"{property.AccessLevel.GetDescription()}{(property.IsStatic ? "static " : "")}{property.Type} {property.Name}";

                // Auto-property
                if (property.BackingField is null)
                {
                    AppendLine($"{header} {{ get; set; }}");
                }
                else
                {
                    using (BeginScope(header))
                    {
                        AppendLine($"get {{ return {property.BackingField.Name}; }}");
                        AppendLine($"set {{ {property.BackingField.Name} = value; }}");
                    }
                }
            }
            else
            {
                var (propertyAccess, getterAccess, setterAccess) = GetAccessLevels(property.Getter?.AccessLevel ?? AccessLevel.Public, property.Setter?.AccessLevel ?? AccessLevel.Public);
                var header = $"{propertyAccess}{(property.IsStatic ? "static " : "")}{property.Type} {property.Name}";
                using (BeginScope(header))
                {
                    if (property.Getter?.Body != null)
                    {
                        using (BeginScope($"{getterAccess}get"))
                        {
                            foreach (var item in property.Getter.Body.Statements)
                            {
                                InnerWrite(item);
                            }
                        }
                    }
                    else
                    {
                        AppendLine($"{getterAccess}get;");
                    }

                    if (property.Setter?.Body != null)
                    {
                        using (BeginScope($"{setterAccess}set"))
                        {
                            foreach (var item in property.Setter.Body.Statements)
                            {
                                InnerWrite(item);
                            }
                        }
                    }
                    else
                    {
                        AppendLine($"{setterAccess}set;");
                    }
                }
            }

            (string propertyAccess, string getterAccess, string setterAccess) GetAccessLevels(AccessLevel getter, AccessLevel setter)
            {
                if (getter == AccessLevel.Internal && setter == AccessLevel.Protected ||
                    getter == AccessLevel.Protected && setter == AccessLevel.Internal)
                {
                    return ("protected internal ", string.Empty, string.Empty);
                }
                else if (getter == setter)
                {
                    return (getter == AccessLevel.Public ? "public " : getter.GetDescription(), string.Empty, string.Empty);
                }
                else if (getter > setter)
                {
                    return (setter == AccessLevel.Public ? "public " : setter.GetDescription(), getter.GetDescription(), string.Empty);
                }
                else if (setter > getter)
                {
                    return (getter == AccessLevel.Public ? "public " : getter.GetDescription(), string.Empty, setter.GetDescription());
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        private void InnerWrite(CSharpMethod method)
        {
            foreach (var item in method.Attributes)
            {
                InnerWrite(item);
            }
            using (BeginScope(method.GetHeader()))
            {
                if (method.Body != null)
                {
                    foreach (var s in method.Body.Statements)
                    {
                        InnerWrite(s);
                    }
                }
            }
        }

        internal class ScopeTracker : IDisposable
        {
            private readonly CSharpCodeWriter writer;

            public ScopeTracker(CSharpCodeWriter writer)
            {
                this.writer = writer;
            }

            public void Dispose()
            {
                writer.EndScope();
            }
        }
    }

}
