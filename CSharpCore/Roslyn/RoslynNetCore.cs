using System;
using System.Collections.Generic;
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

            Console.WriteLine($"The tree is a {root.Kind()} node.");
            Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
            Console.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            foreach (UsingDirectiveSyntax element in root.Usings)
                Console.WriteLine($"\t{element.Name}");
        }
    }
}
