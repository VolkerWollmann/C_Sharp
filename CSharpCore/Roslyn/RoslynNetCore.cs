using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


namespace C_Sharp.Language.Roslyn
{
    internal class CustomWalker : CSharpSyntaxWalker
    {
        static int Tabs = 0;
        public override void Visit(SyntaxNode node)
        {
            Tabs++;
            var indents = new String(' ', Tabs);
            Debug.WriteLine(indents + node.Kind());
            base.Visit(node);
            --Tabs;
        }
    }
    public class MyRoslynNextCore
    {
        public static void Test()
        {
            const string programText =
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

            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            Debug.WriteLine($"The tree is a {root.Kind()} node.");
            Debug.WriteLine($"The tree has {root.Members.Count} elements in it.");
            Debug.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            foreach (UsingDirectiveSyntax element in root.Usings)
                Debug.WriteLine($"\t{element.Name}");

            // Check
            NamespaceDeclarationSyntax nameSpace = (NamespaceDeclarationSyntax)root.Members[0];
            Debug.WriteLine("Namespace:");
            Debug.WriteLine(programText.Substring(nameSpace.FullSpan.Start, nameSpace.FullSpan.Length));

            ClassDeclarationSyntax programClass = (ClassDeclarationSyntax)nameSpace.Members[0];
            Debug.WriteLine("Class:");
            Debug.WriteLine(programText.Substring(programClass.FullSpan.Start, programClass.FullSpan.Length));

            var walker = new CustomWalker();
            walker.Visit(tree.GetRoot());
        }
    }
}
