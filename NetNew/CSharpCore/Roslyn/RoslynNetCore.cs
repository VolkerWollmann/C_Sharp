using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpNew.Roslyn
{
    internal class CustomWalker : CSharpSyntaxWalker
    {
        static int _tabs;
        public override void Visit(SyntaxNode node)
        {
            _tabs++;
            var indents = new String(' ', _tabs);
            if (node != null)
            {
	            Debug.WriteLine(indents + $"{node.Kind(),-20}" + ":" + MyRoslynNextCore.ProgramText
		            .Substring(node.FullSpan.Start, node.FullSpan.Length)
		            .Substring(0, Math.Min(100, node.FullSpan.Length)).Replace("  ", " ")
		            .Replace("\r\n", string.Empty));
	            base.Visit(node);
            }

            --_tabs;
        }
    }
    public abstract class MyRoslynNextCore
    {
        public const string ProgramText =
            @"  using System;
                    using System.Collections;
                    using System.Linq;
                    using System.Text;

                    namespace HelloWorld
                    {
                        class Program
                        {
                            static void Main(string[] args)
                            {
                                Console.WriteLine(""Hello, World!"");
                            }
                        }
                    }";
        public static void Test()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(ProgramText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            Debug.WriteLine($"The tree is a {root.Kind()} node.");
            Debug.WriteLine($"The tree has {root.Members.Count} elements in it.");
            Debug.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            foreach (UsingDirectiveSyntax element in root.Usings)
                Debug.WriteLine($"\t{element.Name}");

            // Check
            NamespaceDeclarationSyntax nameSpace = (NamespaceDeclarationSyntax)root.Members[0];
            Debug.WriteLine("Namespace:");
            Debug.WriteLine(ProgramText.Substring(nameSpace.FullSpan.Start, nameSpace.FullSpan.Length));

            ClassDeclarationSyntax programClass = (ClassDeclarationSyntax)nameSpace.Members[0];
            Debug.WriteLine("Class:");
            Debug.WriteLine(ProgramText.Substring(programClass.FullSpan.Start, programClass.FullSpan.Length));

            var walker = new CustomWalker();
            walker.Visit(tree.GetRoot());
        }
    }
}
